namespace EventDrivenDesign.Rest2.Application.IntegrationEvents
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;
        public UserCreatedIntegrationEventHandler(ILogger<UserCreatedIntegrationEventHandler> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            _logger.LogTrace("Executing event created user integration");
            var createUserCommand = new CreateUserCommand(integrationEvent.UserId, integrationEvent.UserName);
            await _mediator.Send(createUserCommand);
        }
    }
}