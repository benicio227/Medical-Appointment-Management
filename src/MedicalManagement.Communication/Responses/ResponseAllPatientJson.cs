using MedicalAppointmentManagement.Domain.Entities;

namespace MedicalManagement.Communication.Responses;
public class ResponseAllPatientJson
{
    public List<Patient> Patients {  get; set; } = new List<Patient>();
}
