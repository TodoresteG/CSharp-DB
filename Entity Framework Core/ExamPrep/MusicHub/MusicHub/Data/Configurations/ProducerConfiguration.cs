namespace MusicHub.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> producer)
        {
            producer
                .HasMany(p => p.Albums)
                .WithOne(a => a.Producer)
                .HasForeignKey(a => a.ProducerId);
        }
    }
}
