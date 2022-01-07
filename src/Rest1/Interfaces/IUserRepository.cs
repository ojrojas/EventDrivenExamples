using EventDrivenDesign.Rest1.Models;

namespace EventDrivenDesign.Rest1.Interfaces
{   
    public interface IUserRepository
    {
        Task<User> CreateUser(User User, CancellationToken cancellationToken);
        Task<User> UpdateUser(Guid Id, User User, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> ListUsers(CancellationToken cancellationToken);
        Task<User> GetUserById(Guid Id, CancellationToken cancellationToken);
    }
}