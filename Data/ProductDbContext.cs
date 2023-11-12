
using Microsoft.EntityFrameworkCore;
using MintoStore.Models;

namespace MintoStore.Data
{
    public class ProductDbContext : DbContext
    {
       public ProductDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Size> Sizes { get; set; }
    }
}
