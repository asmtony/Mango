using Mango.Services.ShoppingCartApi.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}
