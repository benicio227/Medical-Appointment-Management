using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Login;
public interface ILoginUserUseCase
{
    public Task<ResponseLoginJson> Execute(RequestLoginJson request);
}
