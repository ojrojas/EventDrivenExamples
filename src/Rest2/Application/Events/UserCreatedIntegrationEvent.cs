namespace EventDrivenDesign.Rest2.Application.IntegrationEvents
{
    public record UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public UserCreatedIntegrationEvent(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}