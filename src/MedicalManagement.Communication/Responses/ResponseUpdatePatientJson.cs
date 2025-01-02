namespace MedicalManagement.Communication.Responses;
public class ResponseUpdatePatientJson
{
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
}
