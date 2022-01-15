using EventDrivenDesign.Rest1.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenDesign.Rest1.Data
{
    public class Rest1DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = Environment.GetEnvironmentVariables();
            optionsBuilder.UseNpgsql($"Host={configuration["HostDB"] ?? "localhost"};Database=UsersDb;Username=postgres;Password=userPass");
        }
    }
}