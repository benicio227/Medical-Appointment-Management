namespace MedicalAppointmentManagement.Exception.ExceptionsBase;
public class ApiErrorResponse
{
    public int Status {  get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<string>? Details { get; set; }

}
