using MedicalAppointmentManagement.Application.UseCases.Users.GetAll;
using MedicalAppointmentManagement.Application.UseCases.Users.Register;
using MedicalAppointmentManagement.Application.UseCases.Users.Update;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatientsController : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromServices] IRegisterPatientUseCase useCase, RequestPatientJson request)
    {
        try
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                Status = 400,
                Error = "Validation Error",
                Message = "Erros ocorreram na validação",
                Details = ex.Errors
            });
        }
        catch (SystemException)
        {
            return StatusCode(500, new ApiErrorResponse
            {
                Status = 500,
                Error = "Internal Server Error",
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            });
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        [FromBody] RequestPatientJson request,
        [FromServices] IUpdatePatientUseCase useCase,
        long id)
    {
        try
        {
            var response = await useCase.Execute(request, id);

            return Ok(response);
        }
        catch (PatientNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse
            {
                Status = 404,
                Error = "Not Found",
                Message = ex.Error,
            });
        }
        catch (SystemException)
        {
            return StatusCode(500, new ApiErrorResponse
            {
                Status = 500,
                Error = "Internal Server Error",
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllPatientsUseCase useCase)
    {
        try
        {
            var response = await useCase.Execute();

            if (response is null || !response.Patients.Any())
            {
                return Ok(new List<object>());
            }

            return Ok(response);
        }
        catch (SystemException)
        {
            return StatusCode(500, new ApiErrorResponse
            {
                Status = 500,
                Error = "Internal Server Error",
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde."
            });
        }

    }
}
