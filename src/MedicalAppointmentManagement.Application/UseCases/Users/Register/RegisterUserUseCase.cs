using FluentValidation.Results;
using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Domain.Security.Cryptography;
using MedicalAppointmentManagement.Domain.Tokens;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;


namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase

{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _writeOnlyRepository = userWriteOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseUserJson> Execute(RequestUserJson request)
    {
        var result = Validate(request);

        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };

        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _writeOnlyRepository.AddUser(user);

        //var existActiveEmail = _userReadOnlyRepository.ExistAciveUserWithEmail(request.Email);

        //if (existActiveEmail)
        //{
        //    result.Errors.Add(new ValidationFailure(string.Empty, "Este e-mail já está sendo registrado"));
        //}

        return new ResponseUserJson
        {
            Name = user.Name,
            Email = user.Email,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private ValidationResult Validate(RequestUserJson request)
    {
        var validator = new UserValidator();
        var result = validator.Validate(request);

        return result;

    }
}
