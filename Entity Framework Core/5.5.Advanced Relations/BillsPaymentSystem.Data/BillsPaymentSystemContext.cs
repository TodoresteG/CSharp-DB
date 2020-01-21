namespace BillsPaymentSystem.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class BillsPaymentSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionSting = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=BillPaymentSystemDB;Integrated Security=True";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionSting);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
                => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
