using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Domain.Security.Cryptography;
using MedicalAppointmentManagement.Domain.Tokens;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Login;
public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public LoginUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
    }
    public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException("Email ou senha inválidos");
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch is false)
        {
            throw new InvalidLoginException("Email ou senha inválidos");
        }

        return new ResponseLoginJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
