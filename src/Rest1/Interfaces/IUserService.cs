namespace EventDrivenDesign.Rest1.Interfaces
{   
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto User, CancellationToken cancellationToken);
        Task<UserDto> UpdateUser(Guid Id, UserDto User, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserDto>> ListUsers(CancellationToken cancellationToken);
        Task<UserDto> GetUserById(Guid Id, CancellationToken cancellationToken);
    }
}