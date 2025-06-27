using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.dao;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.exception;


namespace HospitalManagementSystem.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            IHospitalService service = new HospitalServiceImpl();

            while (true)
            {
                Console.WriteLine("\n===== Hospital Management System =====");
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. Update Appointment");
                Console.WriteLine("3. Cancel Appointment");
                Console.WriteLine("4. Get Appointment by ID");
                Console.WriteLine("5. Get Appointments for Patient");
                Console.WriteLine("6. Get Appointments for Doctor");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int choice;
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Appointment newAppt = new Appointment();
                            Console.Write("Enter Patient ID: ");
                            newAppt.PatientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Doctor ID: ");
                            newAppt.DoctorId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Appointment Date (yyyy-MM-dd HH:mm): ");
                            newAppt.AppointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Description: ");
                            newAppt.Description = Console.ReadLine();

                            bool added = service.ScheduleAppointment(newAppt);
                            Console.WriteLine(added ? "Appointment scheduled successfully." : "Failed to schedule appointment.");
                            break;

                        case 2:
                            Appointment updateAppt = new Appointment();
                            Console.Write("Enter Appointment ID to update: ");
                            updateAppt.AppointmentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Patient ID: ");
                            updateAppt.PatientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Doctor ID: ");
                            updateAppt.DoctorId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Appointment Date (yyyy-MM-dd HH:mm): ");
                            updateAppt.AppointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter new Description: ");
                            updateAppt.Description = Console.ReadLine();

                            bool updated = service.UpdateAppointment(updateAppt);
                            Console.WriteLine(updated ? "Appointment updated successfully." : "Update failed.");
                            break;

                        case 3:
                            Console.Write("Enter Appointment ID to cancel: ");
                            int cancelId = int.Parse(Console.ReadLine());
                            bool deleted = service.CancelAppointment(cancelId);
                            Console.WriteLine(deleted ? "Appointment cancelled." : "Cancellation failed.");
                            break;

                        case 4:
                            Console.Write("Enter Appointment ID: ");
                            int apptId = int.Parse(Console.ReadLine());
                            Appointment foundAppt = service.GetAppointmentById(apptId);
                            if (foundAppt == null)
                                Console.WriteLine("Appointment not found.");
                            else
                                Console.WriteLine(foundAppt);
                            break;

                        case 5:
                            Console.Write("Enter Patient ID: ");
                            int patId = int.Parse(Console.ReadLine());
                            List<Appointment> patientAppts = service.GetAppointmentsForPatient(patId);
                            if (patientAppts.Count == 0)
                                throw new PatientNumberNotFoundException($"No appointments found for patient ID {patId}");
                            foreach (var appt in patientAppts)
                                Console.WriteLine(appt);
                            break;

                        case 6:
                            Console.Write("Enter Doctor ID: ");
                            int docId = int.Parse(Console.ReadLine());
                            List<Appointment> doctorAppts = service.GetAppointmentsForDoctor(docId);
                            if (doctorAppts.Count == 0)
                                Console.WriteLine("No appointments found for this doctor.");
                            else
                                foreach (var appt in doctorAppts)
                                    Console.WriteLine(appt);
                            break;

                        case 7:
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (PatientNumberNotFoundException ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
        }
    }
}
