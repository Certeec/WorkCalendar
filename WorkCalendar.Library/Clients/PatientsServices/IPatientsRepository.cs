using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Clients.PatientsServices
{
    public interface IPatientsRepository
    {
        bool AddPatient(Patient patient);
        bool UpdatePatientActiveStatus(int patientId, PatientActiveStatus status);
    }
}
