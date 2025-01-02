namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class AppointmentNotFoundException : SystemException
{
    public string Error {  get; set; }
    public AppointmentNotFoundException(string error)
    {
        Error = error;
    }
}
