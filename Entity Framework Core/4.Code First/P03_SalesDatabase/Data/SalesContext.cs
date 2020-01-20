namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=SalesDB;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany(p => p.Sales).WithOne(s => s.Product);

                entity.Property(p => p.Description).HasDefaultValue("No description");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Email).IsUnicode(false);

                entity.HasMany(c => c.Sales).WithOne(s => s.Customer);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasMany(st => st.Sales).WithOne(s => s.Store);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(s => s.Product).WithMany(p => p.Sales);

                entity.HasOne(s => s.Customer).WithMany(c => c.Sales);

                entity.HasOne(s => s.Store).WithMany(st => st.Sales);

                entity.Property(s => s.Date).HasDefaultValueSql("getdate()");
            });
        }
    }
}
