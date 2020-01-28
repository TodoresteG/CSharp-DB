namespace SoftJail.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OfficerConfiguration : IEntityTypeConfiguration<Officer>
    {
        public void Configure(EntityTypeBuilder<Officer> officer)
        {
            officer
                .HasMany(o => o.OfficerPrisoners)
                .WithOne(op => op.Officer)
                .HasForeignKey(op => op.OfficerId);
        }
    }
}
