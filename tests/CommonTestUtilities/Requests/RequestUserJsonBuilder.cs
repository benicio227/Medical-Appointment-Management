using Bogus;
using MedicalManagement.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestUserJsonBuilder
{
    public static RequestUserJson Build()
    {
        var faker = new Faker();

        var request = new RequestUserJson
        {
            Name = faker.Name.FullName(),
            Email = faker.Internet.Email(),
            Password = GenerateValidPassword()

        };

        return request;
    }

    private static string GenerateValidPassword()
    {
        // Gera uma senha que atende aos critérios de validação
        var faker = new Faker();
        string upper = faker.Random.String2(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"); // Letra maiúscula
        string lower = faker.Random.String2(1, "abcdefghijklmnopqrstuvwxyz"); // Letra minúscula
        string digit = faker.Random.String2(1, "0123456789"); // Número
        string special = faker.Random.String2(1, "!@#$%^&*(),.?\":{}|<>"); // Caractere especial
        string additional = faker.Random.String2(4); // Adicionais para garantir 8 caracteres no total

        // Combina e embaralha os caracteres para criar a senha
        string password = $"{upper}{lower}{digit}{special}{additional}";
        return string.Join("", password.OrderBy(_ => faker.Random.Int())); // Embaralha a senha
    }
}

