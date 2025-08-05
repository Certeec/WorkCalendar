using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Clients.PatientsServices
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly string _connectionString;

        public PatientsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        
        public bool AddPatient(Patient patient)
        {
            const int isActive = 1;
            
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@PatientName", patient.PatientName);
                com.Parameters.AddWithValue("@PatientSecondName", patient.PatientSecondName);
                com.Parameters.AddWithValue("@IsActive", isActive);
                com.CommandText = "INSERT INTO PATIENTS (PatientName, PatientSecondName, IsActive) Values (@PatientName, @PatientSecondName, @IsActive)";
                com.Connection = sqlCon;
                var result = com.ExecuteNonQuery();

                return result > 0 ? true : false;
            }
        }

        public bool UpdatePatientActiveStatus(int patientId, PatientActiveStatus status)
        {
           using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.CommandType = System.Data.CommandType.Text;
                com.Connection = sqlCon;
                com.Parameters.AddWithValue("@Status", (int)status);
                com.Parameters.AddWithValue("@PatientId", patientId);
                com.CommandText = "UPDATE PATIENTS SET IsActive = @Status WHERE PatientId = @PatientId";
                var result = com.ExecuteNonQuery();

                return result > 0 ? true : false;
            }
           
        }
    }
}
