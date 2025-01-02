using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IUserWriteOnlyRepository
{
    public Task AddUser(User user);
}
