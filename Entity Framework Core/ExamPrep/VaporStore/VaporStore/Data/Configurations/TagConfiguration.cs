namespace VaporStore.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> tag)
        {
            tag
                .HasMany(t => t.GameTags)
                .WithOne(gt => gt.Tag)
                .HasForeignKey(gt => gt.TagId);
        }
    }
}
