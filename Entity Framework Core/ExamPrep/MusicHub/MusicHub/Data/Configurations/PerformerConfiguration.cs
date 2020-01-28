namespace MusicHub.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> performer)
        {
            performer
                .HasMany(p => p.PerformerSongs)
                .WithOne(ps => ps.Performer)
                .HasForeignKey(ps => ps.PerformerId);
        }
    }
}
