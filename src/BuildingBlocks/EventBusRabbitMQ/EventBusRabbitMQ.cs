namespace EventDrivenDesign.BuildingBlocks.EventBusRabbitMQ;

public class EventBusRabbitMQ : IEventBus, IDisposable
{
    private string _queueName;
    private const string BROKER_NAME = "eventdrivendesignbus";
    private readonly IRabbitMQPersitentConnection _persistentConnection;
    private ILogger<EventBusRabbitMQ> _logger;
    private readonly IEventBusSubcriptionsManager _eventSubscriptionManager;
    private readonly ILifetimeScope _autofac;

    private IModel _consumerChannel;


    public EventBusRabbitMQ(string queueName,
                            IRabbitMQPersitentConnection persistentConnection,
                            ILogger<EventBusRabbitMQ> logger,
                            ILifetimeScope autofac,
                            IEventBusSubcriptionsManager eventSubscriptionManager)
    {
        _queueName = queueName;
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _autofac = autofac ?? throw new ArgumentNullException(nameof(autofac));
        _eventSubscriptionManager = eventSubscriptionManager ?? throw new ArgumentNullException(nameof(eventSubscriptionManager));
        _consumerChannel = CreateConsumerChannel();
        _eventSubscriptionManager.OnEventRemoved += EventSubscriptionManager_OnEventRemove;
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
            _persistentConnection.TryConnect();

        _logger.LogTrace("Creating consumer channel");
        var channel = _persistentConnection.CreateModel();

        channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");
        channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.CallbackException += (sender, eventArgs) =>
        {
            _logger.LogInformation(eventArgs.Exception, "Recreating RabbitMQ consumer channel");
            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += Consumer_Received;

            _consumerChannel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
        _logger.LogInformation($"Content {message}");

        // fix 
        message = message.Replace("\u0022", "\"");
        message = message.Replace("\n", "");

        try
        {
            if (message.ToLowerInvariant().Contains("throw-fake-exception"))
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
        }

        _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (_eventSubscriptionManager.HasSubscriptionsForEvent(eventName))
        {
            using (var scope = _autofac.BeginLifetimeScope(BROKER_NAME))
            {
                var subscriptions = _eventSubscriptionManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ResolveOptional(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _eventSubscriptionManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }

    public void Dispose()
    {
        if (_consumerChannel != null)
            _consumerChannel.Dispose();

        _eventSubscriptionManager.Clear();

    }

    public void Publish(IntegrationEvent integrationEvent)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        var eventName = integrationEvent.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", integrationEvent.Id, eventName);

        using (var channel = _persistentConnection.CreateModel())
        {
            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", integrationEvent.Id);

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            var body = JsonSerializer.SerializeToUtf8Bytes(integrationEvent, integrationEvent.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            });


            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", integrationEvent.Id);

            channel.BasicPublish(
                exchange: BROKER_NAME,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        }
    }

    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _eventSubscriptionManager.GetEventKey<T>();
        DoInternalSubscription(eventName: eventName);
        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        _eventSubscriptionManager.AddSubscription<T, TH>();
        StartBasicConsume();
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _eventSubscriptionManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _consumerChannel.QueueBind(queue: _queueName,
                                exchange: BROKER_NAME,
                                routingKey: eventName);
        }
    }

    public void UnSubcribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _eventSubscriptionManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _eventSubscriptionManager.RemoveSubscription<T, TH>();
    }


    public void EventSubscriptionManager_OnEventRemove(object sender, string eventName)
    {
        if(!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        using var channel = _persistentConnection.CreateModel();
        channel.QueueBind(
            queue: _queueName,
            exchange:BROKER_NAME,
            routingKey:eventName);
        
        if(_eventSubscriptionManager.IsEmpty)
        {
            _queueName = string.Empty;
            _consumerChannel.Close();
        }
    }
}