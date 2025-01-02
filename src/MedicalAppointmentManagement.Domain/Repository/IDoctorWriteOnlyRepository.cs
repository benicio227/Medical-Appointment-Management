using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IDoctorWriteOnlyRepository
{
    public Task AddDoctor(Doctor doctor);

    public Task Update(Doctor doctor);
}
