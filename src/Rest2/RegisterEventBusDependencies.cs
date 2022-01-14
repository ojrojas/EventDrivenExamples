using EventDrivenDesign.BuildingBlocks.EventBus;
using EventDrivenDesign.BuildingBlocks.EventBus.Abstractions;
using EventDrivenDesign.BuildingBlocks.EventBus.Interfaces;
using EventDrivenDesign.BuildingBlocks.EventBusRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace EventDrivenDesign.Rest2
{
    internal static class RegisterEventBusDependencies
    {
        public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IRabbitMQPersitentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitPersistentConnection>>();
                var hostName = string.Empty;

                if (string.IsNullOrEmpty(configuration["EventBusConnection"]))
                {
                    hostName = configuration["EventBusUserName"] ?? "localhost";
                }

                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                return new DefaultRabbitPersistentConnection(factory, logger);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var queueName = configuration["QueueName"] ?? "eventqueue";
                var persistentConnection = sp.GetRequiredService<IRabbitMQPersitentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventSubscriptionManager = sp.GetRequiredService<IEventBusSubcriptionsManager>();

                return new EventBusRabbitMQ(queueName, persistentConnection, logger, eventSubscriptionManager);
            });

            services.AddSingleton<IEventBusSubcriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}