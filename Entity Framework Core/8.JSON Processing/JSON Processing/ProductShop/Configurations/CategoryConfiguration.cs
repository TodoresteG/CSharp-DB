namespace ProductShop.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .HasMany(c => c.CategoryProducts)
                .WithOne(cp => cp.Category)
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}
