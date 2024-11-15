using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Appointment>Appointments { get; set; }
        public virtual DbSet<Admin>Admins {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define one-to-one relationship between Patient and Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.patient)
                .WithOne(p => p.appointment)  // One Patient has one Appointment
                .HasForeignKey<Appointment>(a => a.PatientId)  // The foreign key is in Appointment
                .OnDelete(DeleteBehavior.Cascade); // No cascade delete

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.doctor)
                .WithMany(d => d.appointment)  // Doctor can have many Appointments
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for Doctor

        
        }
    }
}





