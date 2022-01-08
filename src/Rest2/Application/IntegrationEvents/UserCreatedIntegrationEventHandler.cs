using EventDrivenDesign.BuildingBlocks.EventBus.Abstractions;

namespace EventDrivenDesign.Rest2.Application.IntegrationEvents
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;

        public UserCreatedIntegrationEventHandler(ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            _logger.LogTrace("Executing event created user integration");
            throw new NotImplementedException();
        }
    }
}