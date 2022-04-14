using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Models
{

public class CRUDeliciousContext : DbContext
{
    public CRUDeliciousContext(DbContextOptions options) : base(options) {}
    public DbSet<Dish> Dishes { get; set; }
}

}