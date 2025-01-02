using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IDoctorReadOnlyRepository
{
    public Task<List<Doctor>> GetAll();

    public Task<Doctor?> GetById(long id);

}
