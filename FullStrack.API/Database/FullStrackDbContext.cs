using FullStrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStrack.API.Database
{
    public class FullStrackDbContext : DbContext
    {
        public FullStrackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
