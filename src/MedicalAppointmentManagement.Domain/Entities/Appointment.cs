using System.Text.Json.Serialization;

namespace MedicalAppointmentManagement.Domain.Entities;
public class Appointment
{
    public int Id { get; set; }

    public int PatientId {  get; set; }

    [JsonIgnore]
    public Patient Patient { get; set; } = new Patient(); // Além das informações da entidade Appointment também pego as informações do Patient

    public int DoctorId {  get; set; }

    [JsonIgnore]
    public Doctor Doctor {  get; set; } = new Doctor(); // Além das informações da entidade Appointment também pego as informações do Doctor
    public DateOnly Date {  get; set; }
    public TimeOnly Time { get; set; }

}
