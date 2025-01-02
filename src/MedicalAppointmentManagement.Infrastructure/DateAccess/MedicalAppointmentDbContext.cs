using MedicalAppointmentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentManagement.Infrastructure.DateAccess;
public class MedicalAppointmentDbContext : DbContext
{
    public MedicalAppointmentDbContext(DbContextOptions<MedicalAppointmentDbContext> options) : base(options){ }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<User> Users {  get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
           .HasOne(a => a.Doctor)
           .WithMany(d => d.Appointments)
           .HasForeignKey(a => a.DoctorId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}

