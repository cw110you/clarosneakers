using webstore.Models;
using Microsoft.EntityFrameworkCore;

namespace webstore.Data
{
    public class WebstoreContext : DbContext
    {
        public WebstoreContext(DbContextOptions<WebstoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Member> Members { get; set; }
        //public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<OrderStatus> OrderStatues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<Advertisement>().ToTable("Advertisement");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Catalog>().ToTable("Catalog");
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<State>().ToTable("State");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");
        }
    }
}