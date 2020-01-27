namespace ProductShop.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .HasMany(u => u.ProductsBought)
                .WithOne(pb => pb.Buyer)
                .HasForeignKey(pb => pb.BuyerId);

            user
                .HasMany(u => u.ProductsSold)
                .WithOne(ps => ps.Seller)
                .HasForeignKey(ps => ps.SellerId);
        }
    }
}
