using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalAppointmentManagement.Domain.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
