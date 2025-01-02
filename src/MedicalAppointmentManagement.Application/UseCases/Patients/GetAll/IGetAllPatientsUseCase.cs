using MedicalAppointmentManagement.Domain.Entities;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.GetAll;
public interface IGetAllPatientsUseCase
{
    public Task<ResponseAllPatientJson> Execute();
}
