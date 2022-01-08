using EventDrivenDesign.BuildingBlocks.EventBus.Events;

namespace EventDrivenDesign.Rest2.Application.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public UserCreatedIntegrationEvent(Guid id, string userName)
        {
            UserId = id;
            UserName = userName;
        }
    }
}