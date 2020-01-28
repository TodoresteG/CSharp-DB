namespace MusicHub.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> song)
        {
            song
                .HasMany(s => s.SongPerformers)
                .WithOne(sp => sp.Song)
                .HasForeignKey(sp => sp.SongId);
        }
    }
}
