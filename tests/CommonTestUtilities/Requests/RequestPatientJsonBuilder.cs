using Bogus;
using Bogus.Extensions.Brazil;
using MedicalManagement.Communication.Requests;


namespace CommonTestUtilities.Requests;
public class RequestPatientJsonBuilder
{
    public static RequestPatientJson Build()
    {
        var faker = new Faker();

        var request = new RequestPatientJson
        {
            Name = faker.Name.FullName(),
            DateOfBirth = DateOnly.FromDateTime(faker.Date.Past(100)),
            Cpf = faker.Person.Cpf(),
            Address = faker.Address.FullAddress()
        };

        return request;
    }
}
