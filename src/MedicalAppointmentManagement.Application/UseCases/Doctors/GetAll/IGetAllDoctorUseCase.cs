using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.GetAll;
public interface IGetAllDoctorUseCase
{
    public Task<ResponseAllDoctorJson> Execute();
}
