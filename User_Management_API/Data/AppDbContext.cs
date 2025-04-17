using Microsoft.EntityFrameworkCore;
using User_Management_API.Models;

namespace User_Management_API.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
