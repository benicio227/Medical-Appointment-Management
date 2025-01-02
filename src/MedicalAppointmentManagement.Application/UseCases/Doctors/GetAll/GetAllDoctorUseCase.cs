using MedicalAppointmentManagement.Domain.Repository;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.GetAll;
public class GetAllDoctorUseCase : IGetAllDoctorUseCase
{
    private readonly IDoctorReadOnlyRepository _doctorReadOnlyRepository;
    public GetAllDoctorUseCase(IDoctorReadOnlyRepository doctorReadOnlyRepository)
    {
        _doctorReadOnlyRepository = doctorReadOnlyRepository;
    }
    public async Task<ResponseAllDoctorJson> Execute()
    {
        var doctors = await _doctorReadOnlyRepository.GetAll();

        if (doctors is null || !doctors.Any())
        {
            throw new ArgumentException("Nenhum médico encontrado");
        }

        return new ResponseAllDoctorJson
        {
            Doctors = doctors
        };
    }
}
