using Microsoft.EntityFrameworkCore;
using Pay1App_POS.Models;

namespace Pay1App_POS.Data
{
    public class PosDbContext : DbContext
    {
        public PosDbContext() { }

        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Change Server name if needed (e.g. .\\SQLEXPRESS)
                optionsBuilder.UseSqlServer(
                    @"Server=localhost;Database=Pay1AppPOS;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.ColorHex).HasMaxLength(7);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasIndex(x => x.SKU).IsUnique();
                e.HasIndex(x => x.Barcode).IsUnique();
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.SKU).HasMaxLength(50).IsRequired();
                e.Property(x => x.Barcode).HasMaxLength(50).IsRequired();
                e.Property(x => x.Price).HasPrecision(18, 2);
                e.Property(x => x.Cost).HasPrecision(18, 2);
                e.Property(x => x.ABV).HasPrecision(5, 2);

                e.HasOne(x => x.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(x => x.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}