using FluentValidation.Results;
using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Register;
public class RegisterPatientUseCase : IRegisterPatientUseCase
{
    private readonly MedicalAppointmentDbContext _dbContext;
    private readonly IPatientWriteOnlyRepository _userWriteOnlyRepository;
    public RegisterPatientUseCase(
        MedicalAppointmentDbContext dbContext,
        IPatientWriteOnlyRepository userWriteOnlyRepository)
    {
        _dbContext = dbContext;
        _userWriteOnlyRepository = userWriteOnlyRepository;
    }
    public async Task<ResponsePatientJson> Execute(RequestPatientJson request)
    {
        var validateResult = Validate(request);

        if (validateResult.IsValid is false)
        {
            var errors = validateResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }


        var user = new Patient
        {
            Name = request.Name,
            Cpf = request.Cpf,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
        };

        await _userWriteOnlyRepository.AddUser(user);

        return new ResponsePatientJson
        {
            Name = request.Name,
            Cpf = request.Cpf,
            Address = request.Address
        };
    }

   private ValidationResult Validate(RequestPatientJson request)
    {
        var validator = new PatientValidator();
        return validator.Validate(request);
    }
}
