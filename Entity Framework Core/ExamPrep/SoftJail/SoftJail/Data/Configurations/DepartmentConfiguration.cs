namespace SoftJail.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> department)
        {
            department
                .HasMany(d => d.Cells)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId);
        }
    }
}
