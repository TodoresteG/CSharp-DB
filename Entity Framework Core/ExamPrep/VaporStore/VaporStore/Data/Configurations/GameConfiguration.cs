namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game
                .HasMany(g => g.Purchases)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId);

            game
                .HasMany(g => g.GameTags)
                .WithOne(gt => gt.Game)
                .HasForeignKey(gt => gt.GameId);
        }
    }
}
