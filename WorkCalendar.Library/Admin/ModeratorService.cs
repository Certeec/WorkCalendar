using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkCalendar.Library.Clients.PatientsServices;

namespace WorkCalendar.Library.Admin
{
    public class ModeratorService : IModeratorService
    {
        private readonly IPatientsRepository _patientsRepository;

        public ModeratorService(IPatientsRepository patientRepository)
        {
            _patientsRepository = patientRepository;
        }

        public bool AddPatient(Patient patient)
        {
           return _patientsRepository.AddPatient(patient);
        }

        public bool UpdatePatientActiveStatus(int patientId, PatientActiveStatus status)
        {
            return _patientsRepository.UpdatePatientActiveStatus(patientId, status);
        }
    }
}
