namespace EventDrivenDesign.Rest2.Data
{
    public class Rest2DbContext : DbContext
    {
        public Rest2DbContext() { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = Environment.GetEnvironmentVariables();
            optionsBuilder.UseNpgsql($"Host={configuration["HostDB"] ?? "localhost"};Database=PostsDb;Username=postgres;Password=userPass");
        }
    }
}