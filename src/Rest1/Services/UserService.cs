using AutoMapper;
using EventDrivenDesign.BuildingBlocks.EventBus.Abstractions;
using EventDrivenDesign.Rest1.Application.IntegrationEvents;
using EventDrivenDesign.Rest1.Dtos;
using EventDrivenDesign.Rest1.Interfaces;
using EventDrivenDesign.Rest1.Models;

namespace EventDrivenDesign.Rest1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly ILogger<UserService> _logger;

        public UserService(IEventBus eventBus,IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserDto> CreateUser(UserDto UserDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(UserDto);
            _logger.LogInformation("creating user into api rest1");
            var result = _mapper.Map<UserDto>(await _userRepository.CreateUser(user, cancellationToken));
            var userCreatedIntegrationEvent = new UserCreatedIntegrationEvent(result.Id, result.Name);
            _logger.LogInformation("sending queue info user created");

            _eventBus.Publish(userCreatedIntegrationEvent);
            return  result;
        }

        public async Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken)
        {
            return  await _userRepository.DeleteUser(Id, cancellationToken);
        }

        public async Task<UserDto> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            return  _mapper.Map<UserDto>(await _userRepository.GetUserById(Id, cancellationToken));
        }

        public async Task<IReadOnlyList<UserDto>> ListUsers(CancellationToken cancellationToken)
        {
            return  _mapper.Map<IReadOnlyList<UserDto>>(await _userRepository.ListUsers(cancellationToken));
        }

        public async Task<UserDto> UpdateUser(Guid Id, UserDto User, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(User);
            return  _mapper.Map<UserDto>(await _userRepository.UpdateUser(Id, user, cancellationToken));
        }
    }
}