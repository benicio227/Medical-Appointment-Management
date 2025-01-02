namespace MedicalManagement.Communication.Requests;
public class RequestAppointmentJson
{
    public int PatientId { get; set; }
    public int DoctorId {  get; set; }
    public DateOnly Date {  get; set; }
    public TimeOnly Time { get; set; }
}
