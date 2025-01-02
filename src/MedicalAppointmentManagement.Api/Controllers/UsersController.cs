using MedicalAppointmentManagement.Application.UseCases.Users.Register;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Register([FromBody] RequestUserJson request, [FromServices] IRegisterUserUseCase useCase)
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
       
    }
}
