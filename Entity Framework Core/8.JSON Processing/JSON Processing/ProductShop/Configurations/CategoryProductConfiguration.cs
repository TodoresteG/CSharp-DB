namespace ProductShop.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> categoryProduct)
        {
            categoryProduct
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });
        }
    }
}
