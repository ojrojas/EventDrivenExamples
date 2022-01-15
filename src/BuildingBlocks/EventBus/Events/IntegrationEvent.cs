using System.Text.Json.Serialization;

namespace EventDrivenDesign.BuildingBlocks.EventBus.Events
{
    public class IntegrationEvent
    {
        [JsonConstructor]
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime created)
        {
            Id = id;
            Created = created;
        }

        [JsonInclude]
        public Guid Id { get; private set; }
        [JsonInclude]
        public DateTime Created { get; private set; }
    }
}