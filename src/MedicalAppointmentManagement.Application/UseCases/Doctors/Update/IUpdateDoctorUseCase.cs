using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.Update;
public interface IUpdateDoctorUseCase
{
    public Task<ResponseDoctorJson> Execute(RequestDoctorJson request, long id);
}
