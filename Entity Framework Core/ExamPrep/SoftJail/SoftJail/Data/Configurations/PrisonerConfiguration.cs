namespace SoftJail.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PrisonerConfiguration : IEntityTypeConfiguration<Prisoner>
    {
        public void Configure(EntityTypeBuilder<Prisoner> prisoner)
        {
            prisoner
                .HasMany(p => p.Mails)
                .WithOne(m => m.Prisoner)
                .HasForeignKey(m => m.PrisonerId);

            prisoner
                .HasMany(p => p.PrisonerOfficers)
                .WithOne(po => po.Prisoner)
                .HasForeignKey(po => po.PrisonerId);
        }
    }
}
