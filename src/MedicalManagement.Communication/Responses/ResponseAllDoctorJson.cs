using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalManagement.Communication.Responses;
public class ResponseAllDoctorJson
{
    public List<Doctor> Doctors { get; set; } = new List<Doctor>();
}
