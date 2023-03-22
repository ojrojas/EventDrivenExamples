namespace EventDrivenDesign.Rest1.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Rest1DbContext _context;
        public UserRepository()
        {
            _context = new Rest1DbContext();
        }

        public async Task<User> CreateUser(User User, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(User);
            return await SaveAsync(cancellationToken) ? User : default;
        }

        public async Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            _context.Users.Remove(user);
            return await SaveAsync(cancellationToken);
        }

        public async Task<User> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(new object[] { Id }, cancellationToken);
        }

        public async Task<IReadOnlyList<User>> ListUsers(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> UpdateUser(Guid Id, User User, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            user.Name = User.Name;
            user.LastName = User.LastName;
            user.Email = User.Email;
            user.OtherData = User.OtherData;
            _context.Update(user);
            return await SaveAsync(cancellationToken) ? user : default;
        }

        private async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}