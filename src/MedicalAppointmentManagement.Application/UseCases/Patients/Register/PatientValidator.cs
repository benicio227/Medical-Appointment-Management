using BrazilModels;
using FluentValidation;
using MedicalManagement.Communication.Requests;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public class PatientValidator : AbstractValidator<RequestPatientJson>
{
    public PatientValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("O nome é requerido");

        RuleFor(user => user.Cpf)
            .NotEmpty()
            .WithMessage("O CPF é requerido")
            .DependentRules(() =>
            {
                RuleFor(user => user.Cpf)
                   .Must(cpf => Cpf.TryParse(cpf, out _))
                   .WithMessage("O CPF informado é inválido");
            });
           

        RuleFor(user => user.DateOfBirth)
            .NotEmpty()
            .WithMessage("A data de nascimento é requerida")
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("A data de nascimento deve ser no passado");

        RuleFor(user => user.Address)
            .NotEmpty()
            .WithMessage("O endereço é requerido")
            .DependentRules(() =>
            {
                RuleFor(user => user.Address)
                    .MinimumLength(5)
                    .WithMessage("O endereço deve ter pelo menos 5 caracteres");

            })
            .MaximumLength(100)
            .WithMessage("O endereço deve ter no máximo 100 caracteres");

    }
}
