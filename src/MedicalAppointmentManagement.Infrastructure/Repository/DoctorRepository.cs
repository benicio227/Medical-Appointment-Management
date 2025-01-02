using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentManagement.Infrastructure.Repository;

public class DoctorRepository : IDoctorWriteOnlyRepository, IDoctorReadOnlyRepository
{
    private readonly MedicalAppointmentDbContext _dbContext;

    public DoctorRepository(MedicalAppointmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDoctor(Doctor doctor)
    {
        await _dbContext.AddAsync(doctor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Doctor>> GetAll()
    {
        var doctors = await _dbContext.Doctors.AsNoTracking().ToListAsync();

        return doctors;
    }

    public async Task<Doctor?> GetById(long id)
    {
        var doctor = await _dbContext.Doctors.AsNoTracking().FirstOrDefaultAsync(doctor => doctor.Id == id);

        return doctor;
    }

    public async Task Update(Doctor doctor)
    {
        _dbContext.Doctors.Update(doctor);
        await _dbContext.SaveChangesAsync();

    }
}
