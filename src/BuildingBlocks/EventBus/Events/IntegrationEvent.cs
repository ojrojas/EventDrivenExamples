namespace EventDrivenDesign.BuildingBlocks.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent(Guid id, DateTime created)
        {
            Id = id;
            Created = created;
        }

        public Guid Id { get; private set; }
        public DateTime Created { get; private set; }
    }
}