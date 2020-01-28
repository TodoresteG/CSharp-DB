namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> card)
        {
            card
                .HasMany(c => c.Purchases)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.CardId);
        }
    }
}
