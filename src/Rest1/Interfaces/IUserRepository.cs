using Rest1.Models;

namespace Rest1.Interfaces
{   
    public interface IUserRepository
    {
        Task<User> CreateUser(User User, CancellationToken cancellationToken);
        Task<User> UdateUser(Guid Id, User User, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> ListUsers(CancellationToken cancellationToken);
        Task<User> GetUserById(Guid Id, CancellationToken cancellationToken);
    }
}