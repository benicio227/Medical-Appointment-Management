using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    public Task<ResponseUserJson> Execute(RequestUserJson request);
}
