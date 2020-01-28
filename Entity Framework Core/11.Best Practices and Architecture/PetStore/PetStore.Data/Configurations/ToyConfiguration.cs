namespace PetStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ToyConfiguration : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> toy)
        {
            toy
                .HasMany(t => t.Orders)
                .WithOne(o => o.Toy)
                .HasForeignKey(o => o.ToyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
