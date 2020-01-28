namespace TeisterMask.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeTaskConfiguration : IEntityTypeConfiguration<EmployeeTask>
    {
        public void Configure(EntityTypeBuilder<EmployeeTask> employeeTask)
        {
            employeeTask
                .HasKey(et => new { et.EmployeeId, et.TaskId });
        }
    }
}
