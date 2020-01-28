namespace PetStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order
                .HasMany(o => o.Foods)
                .WithOne(f => f.Order)
                .HasForeignKey(f => f.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            order
                .HasMany(o => o.Toys)
                .WithOne(t => t.Order)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            order
                .HasMany(o => o.Pets)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
