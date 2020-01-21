namespace P01_StudentSystem.Data.Configurations
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {

            student
                .Property(s => s.PhoneNumber)
                .HasColumnType("char(10)")
                .IsUnicode(false)
                .IsRequired(false);

            student
                .HasMany(s => s.CourseEnrollments)
                .WithOne(ce => ce.Student)
                .HasForeignKey(ce => ce.StudentId);

            student
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(hs => hs.Student)
                .HasForeignKey(hs => hs.StudentId);
        }
    }
}
