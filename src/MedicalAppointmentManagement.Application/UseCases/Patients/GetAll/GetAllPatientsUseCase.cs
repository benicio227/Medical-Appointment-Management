using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.GetAll;
public class GetAllPatientsUseCase : IGetAllPatientsUseCase
{
    private readonly MedicalAppointmentDbContext _dbContext;
    private readonly IPatientReadOnlyRepository _userReadOnlyRepository;

    public GetAllPatientsUseCase(
        MedicalAppointmentDbContext dbContext,
        IPatientReadOnlyRepository userReadOnlyRepository)
    {
        _dbContext = dbContext;
        _userReadOnlyRepository = userReadOnlyRepository;
    }
    public async Task<ResponseAllPatientJson> Execute()
    {
        var patients = await _userReadOnlyRepository.GetAll();

        if (patients is null)
        {
            throw new PatientNotFoundException("Não há nenhum paciente para ser exibido");
        }

        return new ResponseAllPatientJson
        {
            Patients = patients,
        };
    }
}
