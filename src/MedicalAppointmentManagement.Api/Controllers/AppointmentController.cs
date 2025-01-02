using MedicalAppointmentManagement.Application.UseCases.Appointments.Delete;
using MedicalAppointmentManagement.Application.UseCases.Appointments.GetById;
using MedicalAppointmentManagement.Application.UseCases.Appointments.Register;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RequestAppointmentJson request, [FromServices] IRegisterAppointmentUseCase useCase)
    {
        try
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(ex.Errors);
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromServices] IGetByIdAppointmentUseCase useCase, long id)
    {
        try
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }
        catch(AppointmentNotFoundException ex)
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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromServices] IDeleteByIdAppointmentUseCase useCase, long id)
    {
        try
        {
            await useCase.Execute(id);

            return NoContent();
        }
        catch(AppointmentNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse
            {
                Status = 404,
                Error = "NotFound",
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
