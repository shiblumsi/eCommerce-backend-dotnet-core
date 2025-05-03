using eCommerce_backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        // Authentication-related tables
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Configure decimal precision and scale for Product.BasePrice
            modelBuilder.Entity<Product>()
                .Property(p => p.BasePrice)
                .HasColumnType("decimal(8, 2)"); 

            // Configure decimal precision and scale for ProductVariant.Price
            modelBuilder.Entity<ProductVariant>()
                .Property(pv => pv.Price)
                .HasColumnType("decimal(8, 2)");

            // One-to-one between ProductVariant and ProductImage
            modelBuilder.Entity<ProductVariant>()
                .HasOne(pv => pv.ProductImage)
                .WithOne(pi => pi.ProductVariant)
                .HasForeignKey<ProductImage>(pi => pi.ProductVarientId);

        }
    }
}
