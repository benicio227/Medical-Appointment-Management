namespace MedicalManagement.Communication.Responses;
public class ResponseAppointmentJson
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
