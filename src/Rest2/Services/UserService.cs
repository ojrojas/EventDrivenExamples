namespace EventDrivenDesign.Rest2.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CreateUser(UserDto userDto, CancellationToken cancellationToken)
        {
            _logger.LogTrace("creating user for event driven rest1");
            var user = _mapper.Map<User>(userDto);
            var result = await _userRepository.CreateUser(user, cancellationToken);
            _logger.LogTrace("sending queue info user created");
            return result;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get user by Id, request");
            return _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(Id, cancellationToken));
        }
        public async Task<IReadOnlyList<UserDto>> ListUser(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get users list by request");
            return _mapper.Map<IReadOnlyList<UserDto>>(await _userRepository.ListUser(cancellationToken));
        }
    }
}