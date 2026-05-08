using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.Models
{
   
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }

            public DbSet<Item> Items { get; set; }

            public DbSet<CartItem> CartItems { get; set; }

            public DbSet<OrderDetails> OrderDetails { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<OrderDetails>()
                    .Property(x => x.Total)
                    .HasComputedColumnSql("[Price] * [Quantity]");
            }
        }
    
}
