using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using System;
using System.Linq;

namespace MVCAgenda.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new AgendaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AgendaContext>>()))
                {
                    if (context.Room.Any() == false)
                    {
                        context.Room.AddRange(
                        new Room
                        {
                            RoomName = "Camera 1 medical"
                        },
                        new Room
                        {
                            RoomName = "Camera 2 medical"
                        },
                        new Room
                        {
                            RoomName = "Camera 3 medical"
                        },
                        new Room
                        {
                            RoomName = "Camera 1 corporal"
                        },
                        new Room
                        {
                            RoomName = "Camera 2 corporal"
                        },
                        new Room
                        {
                            RoomName = "Camera 3 corporal"
                        },
                        new Room
                        {
                            RoomName = "Camera 4 corporal"
                        },
                        new Room
                        {
                            RoomName = "Sala de sport"
                        });
                    }

                    if (context.Medic.Any() == false)
                    {
                        context.Medic.AddRange(
                        new Medic
                        {
                            MedicName = "Doctor Ana-Maria"
                        },
                        new Medic
                        {
                            MedicName = "Asistent Andrei"
                        },
                        new Medic
                        {
                            MedicName = "Asistent Ionela"
                        });
                    }

                    if (context.Consultation.Any() == false)
                    {
                        context.Consultation.AddRange(
                        new Consultation
                        {
                            SheetPatientId = 1,
                            Prescriptions = "sample text Prescriptii 1",
                            Diagnostic = "sample text Diagnostic 1",
                            Symptoms = "sample text Simptome 1",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 1,
                            Prescriptions = "sample text Prescriptii 2",
                            Diagnostic = "sample text Diagnostic 2",
                            Symptoms = "sample text Simptome 2",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 1,
                            Prescriptions = "sample text Prescriptii 3",
                            Diagnostic = "sample text Diagnostic 3",
                            Symptoms = "sample text Simptome 3",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 2,
                            Prescriptions = "sample text Prescriptii 3",
                            Diagnostic = "sample text Diagnostic 3",
                            Symptoms = "sample text Simptome 3",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 3,
                            Prescriptions = "sample text Prescriptii 3",
                            Diagnostic = "sample text Diagnostic 3",
                            Symptoms = "sample text Simptome 3",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 4,
                            Prescriptions = "sample text Prescriptii 3",
                            Diagnostic = "sample text Diagnostic 3",
                            Symptoms = "sample text Simptome 3",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 5,
                            Prescriptions = "sample text Prescriptii 1",
                            Diagnostic = "sample text Diagnostic 1",
                            Symptoms = "sample text Simptome 1",
                            CreationDate = DateTime.Parse("2000-07-24")
                        },
                        new Consultation
                        {
                            SheetPatientId = 5,
                            Prescriptions = "sample text Prescriptii 2",
                            Diagnostic = "sample text Diagnostic 2",
                            Symptoms = "sample text Simptome 2",
                            CreationDate = DateTime.Parse("2000-07-24")
                        });
                        //context.SaveChanges();
                    }

                    if (context.SheetPatient.Any() == false)
                    {
                        context.SheetPatient.AddRange(
                        new SheetPatient
                        {
                            CNP = "1990713123123",
                            PhysicalExamination = "Clinic sanatos",
                            AntecedentsH = "antecedente de la parinti lipsa",
                            AntecedentsP = "acnee",
                            Town = "Suceava",
                            Street = "Calea Unirii numarul 12",
                            Gender = 1,
                            DateOfBirth = DateTime.Parse("1989-02-12")
                        },
                        new SheetPatient
                        {
                            CNP = "1990713123321",
                            PhysicalExamination = "Clinic sanatos",
                            AntecedentsH = "",
                            AntecedentsP = "",
                            Town = "Suceava",
                            Street = "Calea Unirii numarul 21",
                            Gender = 1,
                            DateOfBirth = DateTime.Parse("1934-05-12")
                        },
                        new SheetPatient
                        {
                            CNP = "1990713123098",
                            PhysicalExamination = "Clinic sanatos",
                            AntecedentsH = "",
                            AntecedentsP = "acnee",
                            Town = "Suceava",
                            Street = "Calea Unirii numarul 44",
                            Gender = 1,
                            DateOfBirth = DateTime.Parse("1929-02-12")
                        },
                        new SheetPatient
                        {
                            CNP = "2990713123322",
                            PhysicalExamination = "Clinic sanatos",
                            AntecedentsH = "antecedente de la parinti lipsa",
                            AntecedentsP = "",
                            Town = "Plopeni",
                            Street = "Calea Unirii numarul 12",
                            Gender = 0,
                            DateOfBirth = DateTime.Parse("1989-03-12")
                        },
                        new SheetPatient
                        {
                            CNP = "2990713123123",
                            PhysicalExamination = "Clinic sanatos",
                            AntecedentsH = "",
                            AntecedentsP = "acnee",
                            Town = "Falticeni",
                            Street = "Calea Unirii numarul 55",
                            Gender = 0,
                            DateOfBirth = DateTime.Parse("2000-07-24")
                        });
                    }

                    if (context.Patient.Any() == false)
                    {
                        context.Patient.AddRange(
                        new Patient
                        {
                            SheetPatientId = 1,
                            FirstName = "Serediuc",
                            SecondName = "Alexandru",
                            PhonNumber = "0757541521",
                            Mail = "",
                            Blacklist = 1,
                            Visible = 1
                        },
                        new Patient
                        {
                            SheetPatientId = 2,
                            FirstName = "Serediuc",
                            SecondName = "Constantin",
                            PhonNumber = "0741241712",
                            Mail = "Alex.gsa@yahoo.com",
                            Blacklist = 0,
                            Visible = 1
                        },
                        new Patient
                        {
                            SheetPatientId = 3,
                            FirstName = "Mircea",
                            SecondName = "Andrei",
                            PhonNumber = "0757616511",
                            Mail = "mirceaand@gmail.com",
                            Blacklist = 0,
                            Visible = 1
                        },
                        new Patient
                        {
                            SheetPatientId = 4,
                            FirstName = "Rotaru",
                            SecondName = "Andreea",
                            PhonNumber = "0757254785",
                            Mail = "androt@yahoo.com",
                            Blacklist = 0,
                            Visible = 1
                        },
                        new Patient
                        {
                            SheetPatientId = 5,
                            FirstName = "Andreea",
                            SecondName = "Ana",
                            PhonNumber = "0755767254",
                            Mail = "andana@gmail.com",
                            Blacklist = 0,
                            Visible = 1
                        });
                    }

                    if (context.Appointment.Any() == false)
                    {
                        context.Appointment.AddRange(
                        new Appointment
                        {
                            PatientId = 1,
                            MedicId = 1,
                            RoomId = 1,
                            AppointmentDate = "2021-02-12",
                            AppointmentHour = "12-00",
                            Procedure = "Anestezie",
                            Made = 1,
                            ResponsibleForAppointment = "Administrator",
                            AppointmentCreationDate = "2021-04-14T09:35",
                            Visible = 1
                        },
                        new Appointment
                        {
                            PatientId = 4,
                            MedicId = 3,
                            RoomId = 5,
                            AppointmentDate = "2021-02-12",
                            AppointmentHour = "13-00",
                            Procedure = "Apucuntura",
                            Made = 1,
                            ResponsibleForAppointment = "Administrator",
                            AppointmentCreationDate = "2021-04-14T09:35",
                            Visible = 1
                        },
                        new Appointment
                        {
                            PatientId = 3,
                            MedicId = 2,
                            RoomId = 4,
                            AppointmentDate = "2021-03-12",
                            AppointmentHour = "14-30",
                            Procedure = "Anestezie",
                            Made = 1,
                            ResponsibleForAppointment = "Administrator",
                            AppointmentCreationDate = "2021-04-14T09:35",
                            Visible = 1
                        },
                        new Appointment
                        {
                            PatientId = 2,
                            MedicId = 1,
                            RoomId = 2,
                            AppointmentDate = "2021-02-02",
                            AppointmentHour = "12-00",
                            Procedure = "Anestezie",
                            Made = 1,
                            ResponsibleForAppointment = "Administrator",
                            AppointmentCreationDate = "2021-04-14T09:35",
                            Visible = 1
                        });
                    }

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private static string GetRandomDate()
        {
            var rand = new Random();
            string date = null;
            //("2000-07-24")

            string day, month, year;
            int aux;

            aux = rand.Next(1,29);
            if (aux > 9)
                day = $"{aux}";
            else
                day = $"0{aux}";

            aux = rand.Next(1, 12);
            if (aux > 9)
                month = $"{aux}";
            else
                month = $"0{aux}";
            aux = rand.Next(1, 21);
            if (aux > 9)
                year = $"20{aux}";
            else
                year = $"200{aux}";

            date = $"{year}-{month}-{day}";

            return date;
        }
        private static string GetRandomFullDate()
        {
            var rand = new Random();
            string date = null;
            //"2021-04-14T09:35"

            string day, month, year, ora, min;
            int aux;

            day = $"{rand.Next(1, 29)}";
            aux = rand.Next(1, 12);
            if (aux > 9)
                month = $"{aux}";
            else
                month = $"0{aux}";
            aux = rand.Next(1, 21);
            if (aux > 9)
                year = $"20{aux}";
            else
                year = $"200{aux}";

            aux = rand.Next(0, 23);
            if (aux > 9)
                ora = $"{aux}";
            else
                ora = $"0{aux}";
            aux = rand.Next(0, 59);
            if (aux > 9)
                min = $"{aux}";
            else
                min = $"0{aux}";

            date = $"{year}-{month}-{day}T{ora}:{min}";

            return date;
        }
        private static string GetRandomOra()
        {
            var rand = new Random();
            string date = null;
            //"09:35"

            string ora, min;
            int aux;

            aux = rand.Next(0, 23);
            if (aux > 9)
                ora = $"{aux}";
            else
                ora = $"0{aux}";
            aux = rand.Next(0, 59);
            if (aux > 9)
                min = $"{aux}";
            else
                min = $"0{aux}";

            date = $"{ora}-{min}";

            return date;
        }

        private static string GetRandomCNP()
        {
            var rand = new Random();
            string cnp = $"{rand.Next(0, 1)}";
            for (int i = 1; i <= 12; i++)
            {
                cnp += $"{rand.Next(0, 9)}";
            }

            return cnp;
        }
        private static string GetRandomNumber()
        {
            var rand = new Random();


            string number = $"0";
            for (int i = 1; i <= 9; i++)
            {
                number += $"{rand.Next(0, 9)}";
            }

            return number;
        }
        private static string GetRandomMedic()
        {
            var rand = new Random();
            string[] medici = new string[]{"Doctor Ana-maria","Asistent Andrei", "Asistenta Daniela", "Asistenta Ionela" };

            string medic = medici[rand.Next(0, 3)];

            return medic;
        }
        private static string GetRandomCamera()
        {
            var rand = new Random();
            string[] camere = new string[] { "Camera 1 Corporal", 
            "Camera 2 Corporal",
            "Camera 3 Corporal",
            "Camera 1 medical",
            "Camera 2 medical",
            "Camera 3 medical",
            "RoomDto 4 medical",
            "Sala sport" };

            string camera = camere[rand.Next(0, 7)];

            return camera;
        }
    }
}
