using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.EF
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<ProductOrder> ProductOrders {get; set;}
        
        public ShoppingContext(DbContextOptions<ShoppingContext> options):
                base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .HasKey(p => p.Id);
            modelBuilder.Entity<Order>()
                        .HasKey(o => o.Id);
            modelBuilder.Entity<ProductOrder>()
                        .HasKey(po => po.Id);

            modelBuilder.Entity<ProductOrder>()
                        .HasOne(po => po.Order)
                        .WithMany(o => o.ProductOrders)
                        .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<ProductOrder>()
                        .HasOne(po => po.Product)
                        .WithMany(o => o.ProductOrders)
                        .HasForeignKey(po => po.ProductId);
        } 
    }
}