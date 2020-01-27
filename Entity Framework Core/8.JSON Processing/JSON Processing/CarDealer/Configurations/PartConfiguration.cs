namespace CarDealer.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> part)
        {
            part
                .HasMany(p => p.PartCars)
                .WithOne(pc => pc.Part)
                .HasForeignKey(pc => pc.PartId);
        }
    }
}
