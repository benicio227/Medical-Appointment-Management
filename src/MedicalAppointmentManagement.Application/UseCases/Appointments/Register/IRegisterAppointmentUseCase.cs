using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Appointments.Register;
public interface IRegisterAppointmentUseCase
{
    public Task<ResponseAppointmentJson> Execute(RequestAppointmentJson request);
}
