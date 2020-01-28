namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> genre)
        {
            genre
                .HasMany(g => g.Games)
                .WithOne(gs => gs.Genre)
                .HasForeignKey(gs => gs.GenreId);
        }
    }
}
