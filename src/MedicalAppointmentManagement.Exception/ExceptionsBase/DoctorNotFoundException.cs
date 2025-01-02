namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class DoctorNotFoundException : SystemException
{
    public string Error {  get; set; }
    public DoctorNotFoundException(string error)
    {
        Error = error;
    }
}
