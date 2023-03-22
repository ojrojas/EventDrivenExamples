namespace EventDrivenDesign.Rest2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Rest2DbContext _context;

        public UserRepository(Rest2DbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(User User, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(User, cancellationToken);
            return await SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<User>> ListUser(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        private async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) != default;
        }
    }
}