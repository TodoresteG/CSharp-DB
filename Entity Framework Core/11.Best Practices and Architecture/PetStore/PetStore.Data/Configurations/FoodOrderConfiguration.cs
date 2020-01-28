namespace PetStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class FoodOrderConfiguration : IEntityTypeConfiguration<FoodOrder>
    {
        public void Configure(EntityTypeBuilder<FoodOrder> foodOrder)
        {
            foodOrder
                .HasKey(fo => new { fo.FoodId, fo.OrderId });
        }
    }
}
