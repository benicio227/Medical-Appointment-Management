using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Update;
public interface IUpdatePatientUseCase
{
    public Task<ResponseUpdatePatientJson> Execute(RequestPatientJson request, long id);
}
