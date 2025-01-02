namespace MedicalAppointmentManagement.Application.UseCases.Appointments.Delete;
public interface IDeleteByIdAppointmentUseCase
{
    public Task Execute(long id);
}
