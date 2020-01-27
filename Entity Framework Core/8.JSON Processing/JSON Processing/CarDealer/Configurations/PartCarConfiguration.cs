namespace CarDealer.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PartCarConfiguration : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> partCar)
        {
            partCar
                .HasKey(pc => new { pc.PartId, pc.CarId });
        }
    }
}
