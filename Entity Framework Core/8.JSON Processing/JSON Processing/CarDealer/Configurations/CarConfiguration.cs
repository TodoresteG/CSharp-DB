namespace CarDealer.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> car)
        {
            car
                .HasMany(c => c.Sales)
                .WithOne(s => s.Car)
                .HasForeignKey(s => s.CarId);

            car.HasMany(c => c.PartCars)
                .WithOne(pc => pc.Car)
                .HasForeignKey(pc => pc.CarId);
        }
    }
}
