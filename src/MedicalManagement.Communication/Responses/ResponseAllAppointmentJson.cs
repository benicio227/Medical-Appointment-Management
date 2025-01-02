using MedicalAppointmentManagement.Domain.Enums;

namespace MedicalManagement.Communication.Responses;
public class ResponseAllAppointmentJson
{
    public List<AppointmentDetails> Appointments { get; set; } = new List<AppointmentDetails>();
}

public class AppointmentDetails
{
    public PatientDetails Patient { get; set; } = new PatientDetails();
    public DoctorDetails Doctor { get; set; } = new DoctorDetails();
    public DateOnly Date {  get; set; }
    public TimeOnly Time {  get; set; }

}


public class PatientDetails
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf {  get; set; } = string.Empty;
    public DateOnly? DateOfBirth {  get; set; }
    public string Address {  get; set; } = string.Empty;
}

public class DoctorDetails
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SpecialtyType Specialty { get; set; }
    public string Crm { get; set; } = string.Empty;

}

