using FluentValidation.Results;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.Update;
public class UpdateDoctorUseCase : IUpdateDoctorUseCase
{
    private IDoctorReadOnlyRepository _doctorReadOnlyRepository;
    private IDoctorWriteOnlyRepository _doctorWriteOnlyRepository;

    public UpdateDoctorUseCase(
        IDoctorReadOnlyRepository doctorReadOnlyRepository,
        IDoctorWriteOnlyRepository doctorWriteOnlyRepository)
    {
        _doctorReadOnlyRepository = doctorReadOnlyRepository;
        _doctorWriteOnlyRepository = doctorWriteOnlyRepository; 
    }
    public async Task<ResponseDoctorJson> Execute(RequestDoctorJson request, long id)
    {
        var validateResult = Validate(request);

        if (validateResult.IsValid is false)
        {
            var errors = validateResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }

        var doctor = await _doctorReadOnlyRepository.GetById(id);

        if (doctor == null)
        {
            throw new DoctorNotFoundException("Nenhum médico encontrado");
        }

        doctor.UpdateDetails(request.Name, request.Specialty, request.Crm);

        await _doctorWriteOnlyRepository.Update(doctor);

        return new ResponseDoctorJson
        {
            Name = request.Name,
            Specialty = request.Specialty,
            Crm = doctor.Crm,
        };
    }

    private static ValidationResult Validate(RequestDoctorJson request)
    {
        var validator = new DoctorValidator();
        return validator.Validate(request);
    }
}
