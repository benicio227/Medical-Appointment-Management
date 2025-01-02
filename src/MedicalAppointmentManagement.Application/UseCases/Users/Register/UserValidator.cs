using FluentValidation;
using MedicalManagement.Communication.Requests;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public class UserValidator : AbstractValidator<RequestUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("O nome é requerido");

        RuleFor(user => user.Email).NotEmpty()
            .NotEmpty()
            .WithMessage("O email é requerido")
            .EmailAddress()
            .WithMessage("O email informado não é válido");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("A senha é requerida")
            .MinimumLength(8)
            .WithMessage("A senha deve ter pelo menos 8 caracteres")
            .Matches(@"[A-Z]")
            .WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches(@"[a-z]")
            .WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches(@"\d")
            .WithMessage("A senha deve conter pelo menos um número")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]")
            .WithMessage("A senha deve conter pelo menos um caractere especial");
    }
}
