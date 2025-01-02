using MedicalAppointmentManagement.Application.UseCases.Login;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] RequestLoginJson request, [FromServices] ILoginUserUseCase useCase)
    {
        try
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
        catch (InvalidLoginException ex)
        {
            return Unauthorized(new ApiErrorResponse
            {
                Status = 401,
                Error = "Erro de autorização",
                Message = ex.Error,
            });
        }
    }
}
