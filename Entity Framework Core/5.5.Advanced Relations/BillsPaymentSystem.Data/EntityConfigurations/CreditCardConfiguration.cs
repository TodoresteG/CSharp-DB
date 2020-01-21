namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> creditCard)
        {
            creditCard
                .HasOne(cc => cc.PaymentMethod)
                .WithOne(pm => pm.CreditCard)
                .HasForeignKey<PaymentMethod>(pm => pm.CreditCardId);
        }
    }
}
