using LoadTestingSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LoadTestingSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<LoadTestResult> LoadTestResults { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
