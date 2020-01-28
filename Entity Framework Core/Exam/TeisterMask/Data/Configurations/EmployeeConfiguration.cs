namespace TeisterMask.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employee)
        {
            employee
                .HasMany(e => e.EmployeesTasks)
                .WithOne(et => et.Employee)
                .HasForeignKey(et => et.EmployeeId);
        }
    }
}
