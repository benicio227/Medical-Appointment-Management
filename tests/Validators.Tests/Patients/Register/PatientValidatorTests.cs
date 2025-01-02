using CommonTestUtilities.Requests;
using FluentAssertions;
using MedicalAppointmentManagement.Application.UseCases.Users.Register;

namespace Validators.Tests.Patients.Register;
public class PatientValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O nome é requerido"));
    }


    [Fact]
    public void Error_Cpf_Empty()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Cpf = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O CPF é requerido"));
    }

    [Fact]
    public void Error_Cpf_Invalid()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Cpf = "123456789";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O CPF informado é inválido"));
    }

    [Fact]
    public void Error_Address_Empty()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Address = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O endereço é requerido"));
    }

    [Fact]
    public void Error_Address_TooShort()
    {
        
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Address = "123";

       
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals("O endereço deve ter pelo menos 5 caracteres"));
    }

    [Fact]
    public void Error_Address_TooLong()
    {
        
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.Address = new string('A', 101);

       
        var result = validator.Validate(request);
        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals("O endereço deve ter no máximo 100 caracteres"));
    }

    [Fact]
    public void Error_DateOfBirth_Empty()
    {
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.DateOfBirth = null; 

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals("A data de nascimento é requerida"));
    }

    [Fact]
    public void Error_DateOfBirth_Future()
    {
        
        var validator = new PatientValidator();
        var request = RequestPatientJsonBuilder.Build();
        request.DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        
        var result = validator.Validate(request);

        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals("A data de nascimento deve ser no passado"));
    }
}
