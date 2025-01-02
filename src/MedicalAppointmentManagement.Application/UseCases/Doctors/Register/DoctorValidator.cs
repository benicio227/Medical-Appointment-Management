using FluentValidation;
using MedicalManagement.Communication.Requests;

namespace MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
public class DoctorValidator : AbstractValidator<RequestDoctorJson>
{
    public DoctorValidator()
    {
        RuleFor(doctor => doctor.Name).NotEmpty().WithMessage("O nome é requerido");

        RuleFor(doctor => doctor.Specialty)
            .NotNull()
            .WithMessage("O nome é requerido")
            .IsInEnum()
            .WithMessage("A especialidade informada é inválida");

        RuleFor(doctor => doctor.Crm)
            .NotEmpty()
            .WithMessage("O CRM é obrigatório");


        RuleFor(doctor => doctor.Crm)
           .Length(7, 9)
           .WithMessage("O CRM deve ter entre 7 e 9 caracteres, incluindo o traço e a sigla do estado.")
           .When(doctor => !string.IsNullOrEmpty(doctor.Crm));


        RuleFor(doctor => doctor.Crm)
            .Matches(@"^\d{4,6}-[A-Z]{2}$")
            .WithMessage("O CRM deve estar no formato '123456-SP'")
            .When(doctor => doctor.Crm.Length >= 7 && doctor.Crm.Length <= 9);

       

    }
}
