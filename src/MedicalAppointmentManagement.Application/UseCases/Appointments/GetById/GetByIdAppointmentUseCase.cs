using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Appointments.GetById;
public class GetByIdAppointmentUseCase : IGetByIdAppointmentUseCase
{
    private readonly IAppointmentReadOnlyRepository _appointmentReadOnlyRepository;

    public GetByIdAppointmentUseCase(IAppointmentReadOnlyRepository appointmentReadOnlyRepository)
    {
        _appointmentReadOnlyRepository = appointmentReadOnlyRepository;
    }
    public async Task<ResponseAllAppointmentJson> Execute(long id)
    {
        var appointments = await _appointmentReadOnlyRepository.GetByPatientId(id);

        if (!appointments.Any())
        {
            throw new AppointmentNotFoundException("Nenhuma consulta encontrada para o paciente especificado.");
        }

        return new ResponseAllAppointmentJson
        {
            Appointments = appointments.Select(a => new AppointmentDetails
            {
                Patient = new PatientDetails
                {
                    Id = a.Patient.Id,
                    Name = a.Patient.Name,
                    Cpf = a.Patient.Cpf,
                    DateOfBirth = a.Patient.DateOfBirth,
                    Address = a.Patient.Address,
                },
                Doctor = new DoctorDetails
                {
                    Id = a.Doctor.Id,
                    Name = a.Doctor.Name,
                    Specialty = a.Doctor.Specialty,
                    Crm = a.Doctor.Crm
                },
                Date = a.Date,
                Time = a.Time,
            }).ToList()
        };

        // Veja que eu consigo acessar as propriedades do Patient e do Doctor dentro da entidade Appointment
        // Isso só é possível por 2 motivos: Porque temos as seguintes PROPRIEDADES DE NAVEGAÇÃO na entidade Appointment: 
        // public Patient Patient { get; set; } = new Patient();
        // public Doctor Doctor { get; set; } = new Doctor();
        // Elas servem para criar uma relação entre as entidades Appointment, Patient e Doctor,
        // permitindo que você acesse os dados dessas entidades diretamente a partir de uma instância
        // de Appointment

        // Também acontece porque configuramos o relacionamento entre as tabelas no mapeamento
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //   modelBuilder.Entity<Appointment>()
        //      .HasOne(a => a.Patient)
    }
}
