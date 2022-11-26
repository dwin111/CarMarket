using CarMarket.Domain.Models_Entity_;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            //Database.EnsureCreated();
        }

        public DbSet<Car> Car { get; set; }
    }
}
