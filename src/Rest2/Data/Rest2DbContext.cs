using EventDrivenDesign.Rest2.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenDesign.Rest2.Data
{
    public class Rest2DbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=postDb;Username=root;Password=postPass");
    }
}