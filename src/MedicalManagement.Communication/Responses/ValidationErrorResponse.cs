namespace MedicalManagement.Communication.Responses
{
    public class ValidationErrorResponse
    {
        public string Message {  get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
