using Bogus;
using MedicalAppointmentManagement.Domain.Enums;
using MedicalManagement.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestDoctorJsonBuilder
{
    public static RequestDoctorJson Build()
    {
        var faker = new Faker();

        var stateAbbreviations = new List<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        var request = new RequestDoctorJson
        {
            Name = faker.Name.FullName(),
            Specialty = faker.PickRandom<SpecialtyType>(),
            Crm = $"{faker.Random.Number(100000, 999999)}-{faker.PickRandom(stateAbbreviations)}"
        };

        return request;
    }
}
