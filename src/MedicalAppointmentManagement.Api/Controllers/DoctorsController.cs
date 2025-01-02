using MedicalAppointmentManagement.Application.UseCases.Doctors.GetAll;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Register;
using MedicalAppointmentManagement.Application.UseCases.Doctors.Update;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DoctorsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RequestDoctorJson request, [FromServices] IRegisterDoctorUseCase useCase)
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllDoctorUseCase useCase)
    {
        try
        {
            var response = await useCase.Execute();

            if (response is null || !response.Doctors.Any())
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
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            });
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public IActionResult Update([FromServices] IUpdateDoctorUseCase useCase, long id, [FromBody] RequestDoctorJson request)
    {
        try
        {
            var response = useCase.Execute(request, id);

            return Ok(response);
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                Status = 400,
                Error = "Erros ocorreram na validação",
                Details = ex.Errors
            });
        }
        catch (DoctorNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse
            {
                Status = 404,
                Error = "Not Found",
                Message = ex.Error
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

}
