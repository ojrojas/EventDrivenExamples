namespace EventDrivenDesign.Rest2.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User User, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> ListUser(CancellationToken cancellationToken);
    }
}