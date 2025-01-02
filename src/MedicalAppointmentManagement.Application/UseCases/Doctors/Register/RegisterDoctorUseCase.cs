using FluentValidation.Results;
using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
public class RegisterDoctorUseCase : IRegisterDoctorUseCase
{
    private readonly IDoctorWriteOnlyRepository _doctorWriteOnlyRepository;
    public RegisterDoctorUseCase(IDoctorWriteOnlyRepository userWriteOnlyRepository)
    {
        _doctorWriteOnlyRepository = userWriteOnlyRepository;
    }
    public async Task<ResponseDoctorJson> Execute(RequestDoctorJson request)
    {
        var result = Validate(request);

        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            // [
            //   { 
            //      new ValidationFailure { ErrorMessage = "Nome é obrigatório"},
            //      new ValidationFailure { ErrorMessage = "CPF inválido"}
            //   }
            // ]

            // Explicação: result.Errors É UMA LISTA "List<ValidationFailure>"
            // Cada item da lista é uma instância de new ValidationFailure com uma propriedade ErrorMessage
            // (error =>) Representa a instância new ValidationFailure
            // (error.ErrorMessage) Representa a propriedade do objeto ValidationFailure

            throw new ArgumentException($"Erros de validação: {string.Join(", ", errors)}");
        }


        var doctor = new Doctor
        {
            Name = request.Name,
            Specialty = request.Specialty,
            Crm = request.Crm,
        };


        await _doctorWriteOnlyRepository.AddDoctor(doctor);

        return new ResponseDoctorJson
        {
            Name= request.Name,
            Specialty = request.Specialty,
            Crm = request.Crm,
        };

    }

    private ValidationResult Validate(RequestDoctorJson request)
    {
        var validator = new DoctorValidator();
        return validator.Validate(request);
    }
}
