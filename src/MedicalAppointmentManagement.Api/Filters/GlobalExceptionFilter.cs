using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MedicalAppointmentManagement.Api.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException validationException)
        {
            Console.WriteLine("Error captured: " + string.Join(", ", validationException.Errors));

            var errorResponse = new ValidationErrorResponse
            {
                Message = "Erro na validação dos dados",
                Errors = validationException.Errors
            };

            context.Result = new BadRequestObjectResult(errorResponse);
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new ObjectResult("Erro inesperado")
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
       
    }
}
