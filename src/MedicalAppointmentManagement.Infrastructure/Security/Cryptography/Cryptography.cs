using BC = BCrypt.Net.BCrypt;
using MedicalAppointmentManagement.Domain.Security.Cryptography;

namespace MedicalAppointmentManagement.Infrastructure.Security.Cryptography;
public class Cryptography : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
