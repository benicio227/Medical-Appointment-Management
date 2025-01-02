using CommonTestUtilities.Requests;
using FluentAssertions;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
using MedicalAppointmentManagement.Domain.Enums;
using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace Validators.Tests.Doctors.Register;
public class DoctorValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O nome é requerido"));

    }

    [Fact]
    public void Error_Specialty_Invalid()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();
        request.Specialty = (SpecialtyType)999;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("A especialidade informada é inválida"));
    }

    [Fact]
    public void Error_Name_Crm_Empty()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();
        request.Crm= string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O CRM é obrigatório"));
    }

    [Fact]
    public void Error_Crm_Invalid()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();
        request.Crm = "12353-ba";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O CRM deve estar no formato '123456-SP'"));
    }

    [Fact]
    public void Error_Crm_Length()
    {
        var validator = new DoctorValidator();
        var request = RequestDoctorJsonBuilder.Build();
        request.Crm = "133232323232313-BA";

        var result = validator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("O CRM deve ter entre 7 e 9 caracteres, incluindo o traço e a sigla do estado."));
    }

}
