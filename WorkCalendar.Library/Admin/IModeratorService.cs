using WorkCalendar.Library.Clients.PatientsServices;

namespace WorkCalendar.Library.Admin
{
    public interface IModeratorService
    {
        bool AddPatient(Patient patient);
        bool UpdatePatientActiveStatus(int patientId, PatientActiveStatus status);
    }
}
