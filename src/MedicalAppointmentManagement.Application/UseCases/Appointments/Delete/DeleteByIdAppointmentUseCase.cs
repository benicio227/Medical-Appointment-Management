using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;

namespace MedicalAppointmentManagement.Application.UseCases.Appointments.Delete;
public class DeleteByIdAppointmentUseCase : IDeleteByIdAppointmentUseCase
{
    private readonly IAppointmentWriteOnlyRepository _appointmentWriteOnlyRepository;

    public DeleteByIdAppointmentUseCase(IAppointmentWriteOnlyRepository appointmentWriteOnlyRepository)
    {
        _appointmentWriteOnlyRepository = appointmentWriteOnlyRepository;
    }
    public async Task Execute(long id)
    {
        var appointment = await _appointmentWriteOnlyRepository.DeleteById(id);

        if (appointment is null)
        {
            throw new AppointmentNotFoundException("Nenhuma consulta encontrada com esse id");
        }
    }
}
