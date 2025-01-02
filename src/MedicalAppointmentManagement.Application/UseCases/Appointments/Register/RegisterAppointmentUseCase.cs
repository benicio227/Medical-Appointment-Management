using MedicalAppointmentManagement.Domain.Entities;
using MedicalAppointmentManagement.Domain.Repository;
using MedicalAppointmentManagement.Exception.ExceptionsBase;
using MedicalManagement.Communication.Requests;
using MedicalManagement.Communication.Responses;

namespace MedicalAppointmentManagement.Application.UseCases.Appointments.Register;
public class RegisterAppointmentUseCase : IRegisterAppointmentUseCase
{
    private readonly IAppointmentWriteOnlyRepository _appointmentWriteOnlyRepository;
    private readonly IAppointmentReadOnlyRepository _appointmentReadOnlyRepository;
    private readonly IPatientReadOnlyRepository _patientReadOnlyRepository;
    private readonly IDoctorReadOnlyRepository _doctorReadOnlyRepository;

    public RegisterAppointmentUseCase(
        IAppointmentWriteOnlyRepository appointmentWriteOnlyRepository,
        IAppointmentReadOnlyRepository appointmentReadOnlyRepository,
        IPatientReadOnlyRepository patientReadOnlyRepository,
        IDoctorReadOnlyRepository doctorReadOnlyRepository
        )
    {
        _appointmentWriteOnlyRepository = appointmentWriteOnlyRepository;
        _appointmentReadOnlyRepository = appointmentReadOnlyRepository;
        _patientReadOnlyRepository = patientReadOnlyRepository;
        _doctorReadOnlyRepository = doctorReadOnlyRepository;
        
    }
    public async Task<ResponseAppointmentJson> Execute(RequestAppointmentJson request)
    {
        var patient = await  _patientReadOnlyRepository.GetById(request.PatientId);
        var doctor = await _doctorReadOnlyRepository.GetById(request.DoctorId);


        if (patient is null)
        {
            throw new ArgumentException("Paciente não encontrado");
        }

        if (doctor is null)
        {
            throw new ArgumentException("Médico não encontrado");
        }

        var existingAppointment = await _appointmentReadOnlyRepository.GetByDoctorAndDate(request.DoctorId, request.Date, request.Time);
       
        if (existingAppointment != null)
        {
            throw new ErrorOnValidationException(new List<string>
            {
                $"O Doutor(a) {doctor.Name} já possui uma consulta agendada no dia {request.Date} às {request.Time}."
            });
        }

        var patientAppointment = await _appointmentReadOnlyRepository.GetPatientAndDateTime(request.PatientId, request.Date, request.Time);

        if (patientAppointment != null)
        {
            throw new ErrorOnValidationException(new List<string>
            {
                $"O paciente {patient.Name} já possui uma consulta agendada no dia {request.Date} às {request.Time}"
            });
        }

        var appointment = new Appointment
        {
            Patient = patient,
            Doctor = doctor,
            Date = request.Date,
            Time = request.Time
        };

        await _appointmentWriteOnlyRepository.AddAppointment(appointment);

        return new ResponseAppointmentJson
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            Date = request.Date,
            Time = request.Time
        };
    }
}
