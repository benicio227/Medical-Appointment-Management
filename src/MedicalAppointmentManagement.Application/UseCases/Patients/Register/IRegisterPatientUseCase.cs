using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;


namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public interface IRegisterPatientUseCase
{
    public Task<ResponsePatientJson> Execute(RequestPatientJson request);
}
