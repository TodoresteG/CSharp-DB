namespace TeisterMask.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> task)
        {

            task
                .HasMany(t => t.EmployeesTasks)
                .WithOne(et => et.Task)
                .HasForeignKey(et => et.TaskId);
        }
    }
}
