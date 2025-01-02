namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class InvalidLoginException : SystemException
{
    public string Error {  get; set; } = string.Empty;

    public InvalidLoginException(string error)
    {
        Error = error;
    }
}
