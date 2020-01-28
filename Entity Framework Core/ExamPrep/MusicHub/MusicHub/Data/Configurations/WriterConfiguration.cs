namespace MusicHub.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class WriterConfiguration : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> writer)
        {
            writer
                .HasMany(w => w.Songs)
                .WithOne(s => s.Writer)
                .HasForeignKey(s => s.WriterId);
        }
    }
}
