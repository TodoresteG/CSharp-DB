namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .Property(u => u.Email)
                .IsUnicode(false);

            user
                .Property(u => u.Password)
                .IsUnicode(false);

            user
                .HasMany(u => u.PaymentMethods)
                .WithOne(pm => pm.User)
                .HasForeignKey(pm => pm.UserId);
        }
    }
}
