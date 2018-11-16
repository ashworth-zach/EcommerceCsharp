using Microsoft.EntityFrameworkCore;
    
namespace Ecommerce.Models
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options) : base(options) {}
        public DbSet<User> user { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<Product> product{get;set;}

    }
}