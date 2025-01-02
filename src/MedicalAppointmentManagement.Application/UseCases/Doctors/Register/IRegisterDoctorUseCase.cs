using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
public interface IRegisterDoctorUseCase
{
    public Task<ResponseDoctorJson> Execute(RequestDoctorJson request);
}
