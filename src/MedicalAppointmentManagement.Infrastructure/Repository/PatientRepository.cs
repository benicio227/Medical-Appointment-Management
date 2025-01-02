using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentManagement.Infrastructure.Repository;
public class PatientRepository : IPatientWriteOnlyRepository, IPatientReadOnlyRepository
{
    private readonly MedicalAppointmentDbContext _dbContext;
    public PatientRepository(MedicalAppointmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddUser(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Patient>> GetAll()
    {
        var patients = await _dbContext.Patients.AsNoTracking().ToListAsync();

        return patients;
    }

    public async Task<Patient?> GetById(long id)
    {
        var patient = await _dbContext.Patients.AsNoTracking().FirstOrDefaultAsync(patient => patient.Id == id);

        return patient;
    }

    public async Task Update(Patient patient)
    {
        _dbContext.Patients.Update(patient);
        await _dbContext.SaveChangesAsync();
    }
}
