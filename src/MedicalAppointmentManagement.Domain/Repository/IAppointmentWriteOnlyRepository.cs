using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IAppointmentWriteOnlyRepository
{
    public Task AddAppointment(Appointment appointment);

    public Task<Appointment?> DeleteById(long id);

}
