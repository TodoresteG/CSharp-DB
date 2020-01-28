namespace PetStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ToyOrderConfiguration : IEntityTypeConfiguration<ToyOrder>
    {
        public void Configure(EntityTypeBuilder<ToyOrder> toyOrder)
        {
            toyOrder
                .HasKey(to => new { to.ToyId, to.OrderId });
        }
    }
}
