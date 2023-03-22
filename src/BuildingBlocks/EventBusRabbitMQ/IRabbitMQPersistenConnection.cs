namespace EventDrivenDesign.BuildingBlocks.EventBusRabbitMQ
{
    public interface IRabbitMQPersitentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}