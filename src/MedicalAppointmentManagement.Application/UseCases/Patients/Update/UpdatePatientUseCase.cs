using FluentValidation.Results;
using MedicalAppointmentManagement.Application.UseCases.Users.Register;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalAppointmentManagement.Infrastructure.DateAccess;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Users.Update;
public class UpdatePatientUseCase : IUpdatePatientUseCase
{
    private readonly MedicalAppointmentDbContext _dbContext;
    private readonly IPatientWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IPatientReadOnlyRepository _userReadeOnlyRepository;
    public UpdatePatientUseCase(
        MedicalAppointmentDbContext dbContext,
        IPatientWriteOnlyRepository userWriteOnlyRepository,
        IPatientReadOnlyRepository userReadOnlyRepository)
    {
        _dbContext = dbContext;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadeOnlyRepository = userReadOnlyRepository;
    }
    public async Task<ResponseUpdatePatientJson> Execute(RequestPatientJson request, long id)
    {
        var validateResult = Validate(request);

        if (validateResult.IsValid is false)
        {
            var errors = validateResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }

        var patient = await _userReadeOnlyRepository.GetById(id);

        if (patient is null)
        {
            throw new PatientNotFoundException("Paciente não encontrado");
        }

        // O método UpdateDetails é apenas uma função criada dentro da classe(entidade) Patient que serve
        // para atualizar os dados de um paciente. Ele recebe os novos valores como parâmetros e os atribui
        // às propriedades correspondentes da  própria instância.
        // É como se estivessemos usando o automapper, mas como temos poucas propriedades, não precisamos
        // usar o automapper, essa maneira é mais simples
        patient.UpdateDetails(request.Name, request.Cpf, request.Address, request.DateOfBirth);

        await _userWriteOnlyRepository.Update(patient);

        return new ResponseUpdatePatientJson
        {
            Name = patient.Name,
            Cpf = patient.Cpf,
            Address = patient.Address,
            DateOfBirth = request.DateOfBirth,
        };
       
    }

    private static ValidationResult Validate(RequestPatientJson request)
    {
        var validator = new PatientValidator();
        return validator.Validate(request);
    }
}
