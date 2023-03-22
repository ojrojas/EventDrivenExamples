namespace EventDrivenDesign.Rest1.Application.IntegrationEvents
{
    public record UserCreatedIntegrationEvent : IntegrationEvent
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