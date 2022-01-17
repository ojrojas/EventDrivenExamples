using EventDrivenDesign.Rest2.Dtos;

namespace EventDrivenDesign.Rest2.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserDto userDto, CancellationToken cancellationToken);
        Task<UserDto> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserDto>> ListUser(CancellationToken cancellationToken);
    }

}