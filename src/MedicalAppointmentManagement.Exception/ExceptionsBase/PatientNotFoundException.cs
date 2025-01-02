namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class PatientNotFoundException : SystemException
{
    public string Error {  get; set; } = string.Empty;
    public PatientNotFoundException(string error)
    {
        Error = error;
    }
}
