using CommonTestUtilities.Requests;
using FluentAssertions;
using MedicalAppointmentManagement.Application.UseCases.Users.Register;

namespace Validators.Tests.Users.Register;
public class UserValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new UserValidator();
        var request = RequestUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new UserValidator();
        var request = RequestUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O nome é requerido"));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new UserValidator();
        var request = RequestUserJsonBuilder.Build();
        request.Email = "beniciobrandaohotmail.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O email informado não é válido"));
    }

}
