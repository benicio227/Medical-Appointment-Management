using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IPatientWriteOnlyRepository
{
    public Task AddUser(Patient patient);

    public Task Update(Patient patient);
}
