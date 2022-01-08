using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventDrivenDesign.BuildingBlocks.EventBusRabbitMQ
{
    public class DefaultRabbitPersistentConnection : IRabbitMQPersitentConnection
    {
        private IConnectionFactory _connectionFactory;
        private ILogger<DefaultRabbitPersistentConnection> _logger;
        IConnection _connection;

        object sync_root = new object();
        bool _dispose;

        public DefaultRabbitPersistentConnection(
            IConnectionFactory connectionFactory,
            ILogger<DefaultRabbitPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_dispose;

        public IModel CreateModel()
        {
            if (!IsConnected)
                throw new InvalidOperationException("No RabbitMQ connection are available to perform this action");
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_dispose) return;

            _dispose = true;

            try
            {
                _connection.ConnectionShutdown -= OnConnectionShutDown;
                _connection.CallbackException -= OnCallBackException;
                _connection.ConnectionBlocked -= OnConnectionBlocked;
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }

        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (_dispose) return;
            _logger.LogWarning("RabbitMQ connection is blocked!, trying to re-connect... ");
            TryConnect();
        }

        private void OnCallBackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (_dispose) return;
            _logger.LogWarning("RabbitMQ connection is exception!, trying to re-connect... ");
            TryConnect();
        }

        private void OnConnectionShutDown(object? sender, ShutdownEventArgs e)
        {
            if (_dispose) return;
            _logger.LogWarning("RabbitMQ connection is shutdown, trying to re-connect... ");
            TryConnect();
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect");

            if (_connection == null)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutDown;
                _connection.CallbackException += OnCallBackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}