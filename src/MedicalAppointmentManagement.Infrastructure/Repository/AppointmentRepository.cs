using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentManagement.Infrastructure.Repository;
public class AppointmentRepository : IAppointmentWriteOnlyRepository, IAppointmentReadOnlyRepository
{
    private MedicalAppointmentDbContext _dbContext;

    public AppointmentRepository(MedicalAppointmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAppointment(Appointment appointment)
    {
        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Appointment?> DeleteById(long id)
    {
        var appointment = await  _dbContext.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == id);

        if (appointment == null)
        {
            return null; 
        }

        _dbContext.Appointments.Remove(appointment!);
        await _dbContext.SaveChangesAsync();

        return appointment;
    }

    public Task<Appointment?> GetByDoctorAndDate(long doctorId, DateOnly date, TimeOnly time)
    // Esse método não permiti agendar uma consulta para o mesmo médico no mesmo horário.
    
    {
        return _dbContext.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(appointment => appointment.DoctorId == doctorId && appointment.Date == date && appointment.Time == time);
    }

    public async Task<List<Appointment>> GetByPatientId(long patientId)
    {
        var appointments = await _dbContext.Appointments
            .Include(appointment => appointment.Patient)
            .Include(appointment => appointment.Doctor)
            .Where(appointment => appointment.PatientId == patientId)
            .AsNoTracking()
            .ToListAsync();

        return appointments;
    }

    public Task<Appointment?> GetPatientAndDateTime(long patientId, DateOnly date, TimeOnly time)
    //Verifica se um paciente já possui uma consulta no mesmo dia e horário
    {
        return _dbContext.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(appointment => appointment.PatientId == patientId && appointment.Date == date && appointment.Time == time);
    }

    // O Entity Framework traduz esse código em algo assim: 
    // SELECT a.*, p.*, d.*
    // FROM Appointments a
    // LEFT JOIN Patients p ON a.PatientId = p.Id
    // LEFTJOIN Doctors d ON a.DocotorsId = d.Id
    // WHERE a.Id = 1;

    // OBS: As informações das tabelas Patients e Doctors aparecem em uma consulta de Appointments
    // Porque: No banco de dados, as chaves estrangeiras(PatientId e DoctorId) LIGAM AS TABELAS
    // Porque: No código C#, as propriedades de navegação(Patient e Doctor) informam ao EF que ele deve
    // buscar as entidades relacionadas

    // Sem essas chaves estrangeiras, as informações relacionadas simplesmente não estariam disponíveis no
    // contexto da consulta.
}
