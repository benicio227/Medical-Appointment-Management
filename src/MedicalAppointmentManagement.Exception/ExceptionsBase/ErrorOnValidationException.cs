namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class ErrorOnValidationException : SystemException
{
    public  List<string> Errors {  get; set; } = new List<string>();

    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
