using Invert.Models;
using Microsoft.EntityFrameworkCore;

namespace Invert.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Position { get; set; }

    }
}
