using MedicalAppointmentManagement.Domain.Enums;

namespace MedicalManagement.Communication.Responses;
public class ResponseDoctorJson
{
    public string Name { get; set; } = string.Empty;
    public SpecialtyType Specialty { get; set; }
    public string Crm { get; set; } = string.Empty;
}
