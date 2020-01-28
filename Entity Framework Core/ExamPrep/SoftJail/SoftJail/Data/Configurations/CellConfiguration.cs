namespace SoftJail.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CellConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> cell)
        {
            cell
                .HasMany(c => c.Prisoners)
                .WithOne(p => p.Cell)
                .HasForeignKey(p => p.CellId);
        }
    }
}
