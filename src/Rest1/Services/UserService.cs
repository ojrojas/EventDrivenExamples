using AutoMapper;
using Rest1.Dtos;
using Rest1.Interfaces;
using Rest1.Models;

namespace Rest1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> CreateUser(UserDto UserDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(UserDto);
            return  _mapper.Map<UserDto>(await _userRepository.CreateUser(user, cancellationToken));
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

        public async Task<UserDto> UdateUser(Guid Id, UserDto User, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(User);
            return  _mapper.Map<UserDto>(await _userRepository.UdateUser(Id, user, cancellationToken));
        }
    }
}