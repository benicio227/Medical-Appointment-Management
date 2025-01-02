using MedicalAppointmentManagement.Domain.Enums;

namespace MedicalAppointmentManagement.Domain.Entities;
public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SpecialtyType Specialty { get; set; }
    public string Crm { get; set; } = string.Empty;


    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public Doctor(string name, SpecialtyType specialty, string crm)
    {
        Name = name;
        Specialty = specialty;
        Crm = crm;
    }

    public Doctor() { }

    public void UpdateDetails(string name, SpecialtyType specialtyType, string crm)
    {
        Name = name;
        Specialty = specialtyType;
        Crm = crm;
    }
}
