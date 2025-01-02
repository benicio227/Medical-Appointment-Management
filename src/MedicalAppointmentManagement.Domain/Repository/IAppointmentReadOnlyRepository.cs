using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IAppointmentReadOnlyRepository
{
    public Task<List<Appointment>> GetByPatientId(long id);

    public Task<Appointment?> GetByDoctorAndDate(long doctorId, DateOnly date, TimeOnly Time);

    public Task<Appointment?> GetPatientAndDateTime(long patientId, DateOnly date, TimeOnly time);
}
