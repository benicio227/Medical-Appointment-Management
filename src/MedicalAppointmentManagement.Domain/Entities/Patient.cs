using System.Text.Json.Serialization;

namespace MedicalAppointmentManagement.Domain.Entities;
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments {  get; set; } = new List<Appointment>(); 

    // Construtor explícito
    // Usar quando desejar garantir que todas as propriedades obrigatórias sejam definidas no momento da
    // No construtor explícito ele EXIGE que passemos todas as propriedades, pois se não passar da erro
    // criação do objeto.
    public Patient(string name, string cpf, string adress, DateOnly? dateOfBirth)
    {
        Name = name;
        Cpf = cpf;
        Address = adress;
        DateOfBirth = dateOfBirth;
    }

    // Construtor padrão
    // Usar quando quiser permitir inicializações flexíveis com propriedades opcionais
    // Estou usando porque na classe RegisterUserUseCase estou instanciando a classe Patient preenchendo as proprie
    // dades e não preciso preencher todas as propriedades
    public Patient() { }

    // Veja que o método UpdateDetails está recebendo os parâmetros e repassando para o construtor
    // explícito Patient, preenchendo suas propriedades
    public void UpdateDetails(string name, string cpf, string address, DateOnly? dateOfBirth)
    {
        Name = name;
        Cpf = cpf;
        Address = address;
        DateOfBirth = dateOfBirth;
    }
}

