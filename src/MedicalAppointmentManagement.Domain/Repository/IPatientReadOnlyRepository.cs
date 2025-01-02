using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IPatientReadOnlyRepository
{
    public Task<List<Patient>> GetAll();

    public Task<Patient?> GetById(long id);
}
