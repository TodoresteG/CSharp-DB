namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> developer)
        {
            developer
                .HasMany(d => d.Games)
                .WithOne(g => g.Developer)
                .HasForeignKey(g => g.DeveloperId);
        }
    }
}
