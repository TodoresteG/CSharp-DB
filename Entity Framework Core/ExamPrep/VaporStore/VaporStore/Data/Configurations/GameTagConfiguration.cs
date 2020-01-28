namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GameTagConfiguration : IEntityTypeConfiguration<GameTag>
    {
        public void Configure(EntityTypeBuilder<GameTag> gameTag)
        {
            gameTag
                .HasKey(gt => new { gt.GameId, gt.TagId });
        }
    }
}
