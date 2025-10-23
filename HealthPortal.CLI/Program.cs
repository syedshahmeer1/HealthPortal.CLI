using System;
using HealthPortalLibrary.models;
using HealthPortalLibrary;
namespace CLI.WordPress
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Health Portal");
            bool cont = true;
            do
            {
                Console.WriteLine("C- Patient Management");
                Console.WriteLine("P- Physician Management");
                Console.WriteLine("A- Appointment Management");
                Console.WriteLine("Q- Quit");
                var input = Console.ReadLine();
                if (input == "C" || input == "c")
                {

                    Console.WriteLine("C- Create New Patient");
                    Console.WriteLine("R- List all Patients");
                    Console.WriteLine("U- Update Patient");
                    Console.WriteLine("D- Delete Patient");
                    var inputPatient = Console.ReadLine();
                    if (inputPatient == "C" || inputPatient == "c")
                    {
                        var patient = new Patients();
                        Console.WriteLine("Enter Patient Name:");
                        patient.Name = Console.ReadLine();
                        Console.WriteLine("Enter Patient Address:");
                        patient.Address = Console.ReadLine();
                        Console.WriteLine("Enter Patient BirthDate (yyyy-MM-dd):");
                        var bdText = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(bdText))
                        {
                            if (DateTime.TryParseExact(bdText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var bd))
                                patient.BirthDate = bd;
                            else
                                Console.WriteLine("Invalid date format — birthdate left empty.");
                        }
                        else
                        {
                            patient.BirthDate = null;
                        }
                        Console.WriteLine("Enter Patient Race:");
                        patient.Race = Console.ReadLine();
                        Console.WriteLine("Enter Patient Gender:");
                        patient.Gender = Console.ReadLine();
                        Console.WriteLine("Add Medical Note for Patient");
                        var note = new MedicalNote();
                        Console.WriteLine("Enter Diagnosis:");
                        note.Diagnosis = Console.ReadLine();
                        Console.WriteLine("Enter Prescription:");
                        note.Prescription = Console.ReadLine();
                        patient.Notes.Add(note);
                        Patientsserviceproxy.Instance.AddOrUpdate(patient);
                        Console.WriteLine("Patient Created:");
                    }
                    else if (inputPatient == "R" || inputPatient == "r")
                    {
                        Console.WriteLine("List of Patients:");
                        foreach (var pat in Patientsserviceproxy.Instance.Patientss)
                        {
                            Console.WriteLine(pat);
                        }
                    }
                    else if (inputPatient == "U" || inputPatient == "u")
                    {
                        Patientsserviceproxy.Instance.Patientss.ForEach(b => Console.WriteLine(b));

                        Console.WriteLine("Enter the ID of the patient to update:");
                        var selection = Console.ReadLine();
                        if (int.TryParse(selection ?? "0", out int IntSelection))
                        {
                            var patientToUpdate = Patientsserviceproxy.Instance.Patientss
                                .FirstOrDefault(b => (b?.Id ?? -1) == IntSelection);
                            if (patientToUpdate != null)
                            {
                                patientToUpdate.Name = Console.ReadLine();
                                patientToUpdate.Address = Console.ReadLine();
                             Console.WriteLine("Enter Patient BirthDate (yyyy-MM-dd):");
                        var bdText = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(bdText))
                        {
                            if (DateTime.TryParseExact(bdText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var bd))
                                patientToUpdate.BirthDate = bd;
                            else
                                Console.WriteLine("Invalid date format — birthdate left empty.");
                        }
                        else
                        {
                            patientToUpdate.BirthDate = null;
                        }
                                patientToUpdate.Race = Console.ReadLine();
                                patientToUpdate.Gender = Console.ReadLine();
                            }
                            Patientsserviceproxy.Instance.AddOrUpdate(patientToUpdate);
                        }
                    }
                    else if (inputPatient == "D" || inputPatient == "d")
                    {
                        Patientsserviceproxy.Instance.Patientss.ForEach(b => Console.WriteLine(b));
                        Console.WriteLine("Enter the ID of the patient to delete:");
                        var selection = Console.ReadLine();
                        if (int.TryParse(selection ?? "0", out int IntSelection))
                        {
                            Patientsserviceproxy.Instance.Delete(IntSelection);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please try again.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                else if (input == "P" || input == "p")
                {
                    Console.WriteLine("C- Create New Physician");
                    Console.WriteLine("R- List all Physicians");
                    Console.WriteLine("U- Update Physician");
                    Console.WriteLine("D- Delete Physician");
                    var inputPhysician = Console.ReadLine();
                    if (inputPhysician == "C" || inputPhysician == "c")
                    {
                        var physician = new Physicians();
                        Console.WriteLine("Enter Physician Name:");
                        physician.Name = Console.ReadLine();
                        Console.WriteLine("Enter Physician License Number:");
                        physician.LicenseNumber = Console.ReadLine();

                        while (true)
                        {
                            Console.WriteLine("Enter Physician Graduation Date (yyyy-MM-dd) or leave blank:");
                            var gdText = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(gdText))
                            {
                                physician.GraduationDate = null;
                                break;
                            }
                            if (DateOnly.TryParseExact(gdText.Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var gd))
                            {
                                // assign DateOnly directly
                                physician.GraduationDate = gd;
                                break;
                            }
                            Console.WriteLine("Invalid date format. Use yyyy-MM-dd. Try again.");
                        }

                        Console.WriteLine("Enter Physician Specializations (comma separated):");
                        physician.Specializations = Console.ReadLine();
                        Physicianserviceproxy.Instance.AddOrUpdate(physician);
                        Console.WriteLine("Physician Created:");
                    }
                    else if (inputPhysician == "R" || inputPhysician == "r")
                    {
                        Console.WriteLine("List of Physicians:");
                        foreach (var phy in Physicianserviceproxy.Instance.Physicians)
                        {
                            Console.WriteLine(phy);
                        }
                    }
                    else if (inputPhysician == "U" || inputPhysician == "u")
                    {
                        Physicianserviceproxy.Instance.Physicians.ForEach(b => Console.WriteLine(b));

                        Console.WriteLine("Enter the ID of the physician to update:");
                        var selection = Console.ReadLine();
                        if (int.TryParse(selection ?? "0", out int IntSelection))
                        {
                            var physicianToUpdate = Physicianserviceproxy.Instance.Physicians
                                .FirstOrDefault(b => (b?.Id ?? -1) == IntSelection);
                            if (physicianToUpdate != null)
                            {
                                physicianToUpdate.Name = Console.ReadLine();
                                physicianToUpdate.LicenseNumber = Console.ReadLine();

                                // update graduation date: allow leaving blank to keep existing
                                Console.WriteLine("Enter Physician Graduation Date (yyyy-MM-dd) ");
                                var gdUpdate = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(gdUpdate))
                                {
                                    if (DateOnly.TryParseExact(gdUpdate.Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var gd))
                                    {
                                        physicianToUpdate.GraduationDate = gd;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid date format — graduation date not changed.");
                                    }
                                }

                                physicianToUpdate.Specializations = Console.ReadLine();
                            }
                            Physicianserviceproxy.Instance.AddOrUpdate(physicianToUpdate);
                        }

                    }
                    else if (inputPhysician == "D" || inputPhysician == "d")
                    {
                        Physicianserviceproxy.Instance.Physicians.ForEach(b => Console.WriteLine(b));
                        Console.WriteLine("Enter the ID of the physician to delete:");
                        var selection = Console.ReadLine();
                        if (int.TryParse(selection ?? "0", out int IntSelection))
                        {
                            Physicianserviceproxy.Instance.Delete(IntSelection);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please try again.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }

                }
                else if (input == "A" || input == "a")
                {
                    Console.WriteLine("C- Create New Appointment");
                    Console.WriteLine("R- List all Appointments");
                    Console.WriteLine("U- Update Appointment");
                    Console.WriteLine("D- Delete Appointment");
                    var inputAppt = Console.ReadLine();

                    if (inputAppt == "C" || inputAppt == "c")
                    {
                        var appt = new Appointments();

                        Console.WriteLine("Enter Patient ID:");
                        var pidText = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(pidText)) { Console.WriteLine("Invalid Input"); }
                        else
                        {
                            appt.PatientId = int.Parse(pidText);

                            Console.WriteLine("Enter Physician ID:");
                            var didText = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(didText)) { Console.WriteLine("Invalid Input"); }
                            else
                            {
                                appt.PhysicianId = int.Parse(didText);

                                // read date and time in a loop until valid
                                DateTime startDate;
                                while (true)
                                {
                                    Console.WriteLine("Enter appointment date (yyyy-MM-dd):");
                                    var dateStr = Console.ReadLine()?.Trim();
                                    Console.WriteLine("Enter start time (HH:mm):");
                                    var timeStr = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrWhiteSpace(dateStr) || string.IsNullOrWhiteSpace(timeStr))
                                    {
                                        Console.WriteLine("Please provide both date and time.");
                                        continue;
                                    }

                                    if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var datePart))
                                    {
                                        Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
                                        continue;
                                    }

                                    if (!TimeSpan.TryParse(timeStr, out var timePart))
                                    {
                                        Console.WriteLine("Invalid time format. Use HH:mm.");
                                        continue;
                                    }

                                    startDate = datePart.Date + timePart;
                                    break;
                                }

                                appt.Start = startDate;

                                Console.WriteLine("Enter duration in minutes (default 60):");
                                var durText = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(durText)) appt.DurationMinutes = int.Parse(durText);

                                var created = Appointmentserviceproxy.Instance.AddOrUpdate(appt);
                                if (created == null)
                                {
                                    Console.WriteLine("Appointment not created (outside hours or physician double-booked).");
                                }
                                else
                                {
                                    Console.WriteLine("Appointment Created:");
                                    Console.WriteLine(created);
                                }
                            }
                        }
                    }
                    else if (inputAppt == "R" || inputAppt == "r")
                    {
                        Console.WriteLine("List of Appointments:");
                        foreach (var a in Appointmentserviceproxy.Instance.Appointmentss)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    else if (inputAppt == "U" || inputAppt == "u")
                    {
                        Appointmentserviceproxy.Instance.Appointmentss.ForEach(a => Console.WriteLine(a));
                        Console.WriteLine("Enter the ID of the appointment to update:");
                        var selection = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(selection)) { Console.WriteLine("Invalid Input"); }
                        else
                        {
                            var apptToUpdate = Appointmentserviceproxy.Instance.Appointmentss
                                .FirstOrDefault(a => (a?.Id ?? -1) == int.Parse(selection));

                            if (apptToUpdate != null)
                            {
                                Console.WriteLine("Enter new date (yyyy-MM-dd) or leave blank to keep:");
                                var newDate = Console.ReadLine();
                                Console.WriteLine("Enter new start time (HH:mm) or leave blank to keep:");
                                var newTime = Console.ReadLine();
                                Console.WriteLine("Enter new duration in minutes or leave blank to keep:");
                                var newDur = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(newDate) || !string.IsNullOrWhiteSpace(newTime))
                                {
                                    var d = string.IsNullOrWhiteSpace(newDate) ? apptToUpdate.Start.ToString("yyyy-MM-dd") : newDate;
                                    var t = string.IsNullOrWhiteSpace(newTime) ? apptToUpdate.Start.ToString("HH:mm") : newTime;
                                    apptToUpdate.Start = DateTime.Parse(d + " " + t);
                                }

                                if (!string.IsNullOrWhiteSpace(newDur))
                                {
                                    apptToUpdate.DurationMinutes = int.Parse(newDur);
                                }

                                var updated = Appointmentserviceproxy.Instance.AddOrUpdate(apptToUpdate);
                                if (updated == null)
                                {
                                    Console.WriteLine("Update failed (outside hours or physician double-booked).");
                                }
                                else
                                {
                                    Console.WriteLine("Appointment Updated:");
                                    Console.WriteLine(updated);
                                }
                            }
                        }
                    }
                    else if (inputAppt == "D" || inputAppt == "d")
                    {
                        Appointmentserviceproxy.Instance.Appointmentss.ForEach(a => Console.WriteLine(a));
                        Console.WriteLine("Enter the ID of the appointment to delete:");
                        var selection = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(selection)) { Console.WriteLine("Invalid Input"); }
                        else
                        {
                            var deleted = Appointmentserviceproxy.Instance.Delete(int.Parse(selection));
                            if (deleted != null) Console.WriteLine("Appointment deleted.");
                            else Console.WriteLine("Appointment not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                else if (input == "Q" || input == "q")
                {
                    cont = false;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }


                   


            }
            while (cont);

        }
    }
}