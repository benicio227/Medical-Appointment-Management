using MedicalAppointmentManagement.Domain.Enums;

namespace MedicalManagement.Communication.Requests;
public class RequestDoctorJson
{
    public string Name { get; set; } = string.Empty;
    public SpecialtyType Specialty { get; set; }
    public string Crm { get; set; } = string.Empty;
}
