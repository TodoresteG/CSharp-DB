namespace PetStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> food)
        {
            food
                .HasMany(f => f.Orders)
                .WithOne(o => o.Food)
                .HasForeignKey(o => o.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
