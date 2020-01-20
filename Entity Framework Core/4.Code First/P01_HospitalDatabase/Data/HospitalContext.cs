namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=HospitalDB;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.Email).IsUnicode(false);

                entity.HasMany(p => p.Visitations).WithOne(v => v.Patient);

                entity.HasMany(p => p.Diagnoses).WithOne(d => d.Patient);

                entity.HasMany(p => p.Prescriptions).WithOne(pr => pr.Patient);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasOne(v => v.Patient).WithMany(p => p.Visitations);

                entity.HasOne(v => v.Doctor).WithMany(d => d.Visitations);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasMany(m => m.Prescriptions).WithOne(pr => pr.Medicament);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(pm => new { pm.PatientId, pm.MedicamentId });
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasMany(d => d.Visitations).WithOne(v => v.Doctor);
            });
        }
    }
}
