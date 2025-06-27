using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.util;

namespace HospitalManagementSystem.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        private readonly string propFile = "db.properties";

        private SqlConnection GetDbConnection()
        {
            return DBConnUtil.GetConnection(propFile);
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            Appointment appointment = null;
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return null;

                string query = "SELECT * FROM Appointment WHERE AppointmentId = @AppointmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        appointment = new Appointment
                        {
                            AppointmentId = reader.GetInt32(0),
                            PatientId = reader.GetInt32(1),
                            DoctorId = reader.GetInt32(2),
                            AppointmentDate = reader.GetDateTime(3),
                            Description = reader.GetString(4)
                        };
                    }
                }
            }
            return appointment;
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return appointments;

                string query = "SELECT * FROM Appointment WHERE PatientId = @PatientId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentId = reader.GetInt32(0),
                            PatientId = reader.GetInt32(1),
                            DoctorId = reader.GetInt32(2),
                            AppointmentDate = reader.GetDateTime(3),
                            Description = reader.GetString(4)
                        });
                    }
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return appointments;

                string query = "SELECT * FROM Appointment WHERE DoctorId = @DoctorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentId = reader.GetInt32(0),
                            PatientId = reader.GetInt32(1),
                            DoctorId = reader.GetInt32(2),
                            AppointmentDate = reader.GetDateTime(3),
                            Description = reader.GetString(4)
                        });
                    }
                }
            }
            return appointments;
        }

        public bool ScheduleAppointment(Appointment appointment)
        {
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return false;

                string query = @"INSERT INTO Appointment (PatientId, DoctorId, AppointmentDate, Description) 
                                 VALUES (@PatientId, @DoctorId, @AppointmentDate, @Description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@Description", appointment.Description);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return false;

                string query = @"UPDATE Appointment 
                                 SET PatientId = @PatientId, DoctorId = @DoctorId, 
                                     AppointmentDate = @AppointmentDate, Description = @Description 
                                 WHERE AppointmentId = @AppointmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@Description", appointment.Description);
                cmd.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            using (SqlConnection conn = GetDbConnection())
            {
                if (conn == null) return false;

                string query = "DELETE FROM Appointment WHERE AppointmentId = @AppointmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
