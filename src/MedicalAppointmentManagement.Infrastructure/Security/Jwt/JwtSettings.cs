namespace MedicalAppointmentManagement.Infrastructure.Security.Jwt;
public class JwtSettings
{
    public string SigningKey {  get; set; } = string.Empty;
    public uint ExpiresMinutes {  get; set; }
}
