
using WorkCalendar.Library.Clients.PatientsServices;

namespace WorkCalendar.Api.ControllersDTO
{
    public class PatientIsActiveStatusDTO
    {
        public int PatientId { get; set; }
        public PatientActiveStatus Status { get; set; }
    }
}
