using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Models;

namespace Mobile_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Wishlist> Wishlists { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ensure explicit table names
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<OrderItem>().ToTable("OrderItems");
            builder.Entity<CartItem>().ToTable("CartItems");
            builder.Entity<Wishlist>().ToTable("Wishlists");
            builder.Entity<Review>().ToTable("Reviews");

            // Seed some review dummy data (optional safe seed)
            builder.Entity<Review>().HasData(
                new Review { Id = 1, ProductId = 1, UserId = null, Rating = 5, Comment = "Excellent phone!" },
                new Review { Id = 2, ProductId = 2, UserId = null, Rating = 4, Comment = "Good value for money." }
            );
        }
    }
}
