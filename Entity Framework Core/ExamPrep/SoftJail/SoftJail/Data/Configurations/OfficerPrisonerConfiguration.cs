namespace SoftJail.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OfficerPrisonerConfiguration : IEntityTypeConfiguration<OfficerPrisoner>
    {
        public void Configure(EntityTypeBuilder<OfficerPrisoner> officerPrisoner)
        {
            officerPrisoner
                .HasKey(op => new { op.OfficerId, op.PrisonerId });
        }
    }
}
