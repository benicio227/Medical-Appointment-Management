namespace MedicalAppointmentManagement.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    public string Encrypt(string password);

    public bool Verify(string password, string passwordHash);
}
