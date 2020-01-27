namespace ProductShop.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product
                .HasMany(p => p.CategoryProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}
