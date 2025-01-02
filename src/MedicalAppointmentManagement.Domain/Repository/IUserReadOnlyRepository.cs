using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Repository;
public interface IUserReadOnlyRepository
{
    public Task<bool> ExistAciveUserWithEmail(string email);

    public Task<User?> GetUserByEmail(string email);
}
