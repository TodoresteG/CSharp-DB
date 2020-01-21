namespace P01_StudentSystem.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course
                .Property(c => c.Description)
                .IsRequired(false);

            course
                .HasMany(c => c.StudentsEnrolled)
                .WithOne(se => se.Course)
                .HasForeignKey(se => se.CourseId);

            course
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            course
                .HasMany(c => c.HomeworkSubmissions)
                .WithOne(hs => hs.Course)
                .HasForeignKey(hs => hs.CourseId);
        }
    }
}
