using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Appointments.GetById;
public interface IGetByIdAppointmentUseCase
{
    public Task<ResponseAllAppointmentJson> Execute(long id);
}
