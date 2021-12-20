using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Enum;
using MVCAgenda.Core.Users;
using MVCAgenda.Core.Users.AppPermissions;
using MVCAgenda.Data.DataBaseManager;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCAgenda.Data.DataBaseManager.Seeds
{
    public class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var init = false;
                if (init)
                {
                    int ressult; 
                    var random = new Random();
                    DateTime startTime;

                    using (var context = new AgendaContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<AgendaContext>>()))
                    {
                    
                        if (context.Rooms.Any() == false)
                        {
                            context.Rooms.AddRange(
                            new Room
                            {
                                Name = "Camera 1 medical",
                                Description = "Camera consultatii",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 2 medical",
                                Description = "Camera Operatii",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 3 medical",
                                Description = "Camera 3 medical description",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 1 corporal",
                                Description = "Camera laser",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 2 corporal",
                                Description = "Camera 2 corporal descriprion",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 3 corporal",
                                Description = "Camera Aparatura",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Camera 4 corporal",
                                Description = "Camera reimprospatare",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            },
                            new Room
                            {
                                Name = "Sala de sport",
                                Description = "Camera refacere musculara",
                                PrimaryColor = "#7f73bb",
                                SecondaryColor = "#fafafa",
                                Hidden = false
                            });

                            context.SaveChanges();
                        }

                        if (context.Medics.Any() == false)
                        {
                            context.Medics.AddRange(
                            new Medic
                            {
                                Name = "Ana Maria",
                                Mail = "ana_maria@yahoo.com",
                                ImagePath = "",
                                Description = "Simpla descriere",
                                Designation = "Doctor",
                                Hidden = false
                            },
                            new Medic
                            {
                                Name = "Andrei",
                                Mail = "andrei@yahoo.com",
                                ImagePath = "",
                                Description = "Se ocupa de programari",
                                Designation = "Nurse",
                                Hidden = false
                            },
                            new Medic
                            {
                                Name = "Ionela",
                                Mail = "ionela@yahoo.com",
                                ImagePath = "",
                                Description = "Se ocupa de tratamentele laser",
                                Designation = "Nurse",
                                Hidden = false
                            });

                            context.SaveChanges();
                        }

                        if (context.Patients.Any() == false)
                        {
                            context.Patients.AddRange(
                            new Patient
                            {
                                FirstName = "Serediuc",
                                LastName = "Alexandru",
                                PhoneNumber = "0757541521",
                                Mail = "",
                                StatusCode = (int)PatientStatus.LoyalPatient,
                                Hidden = false
                            },
                            new Patient
                            {
                                FirstName = "Serediuc",
                                LastName = "Constantin",
                                PhoneNumber = "0741241712",
                                Mail = "Alex.gsa@yahoo.com",
                                StatusCode = (int)PatientStatus.Blacklist,
                                Hidden = false
                            },
                            new Patient
                            {
                                FirstName = "Mircea",
                                LastName = "Andrei",
                                PhoneNumber = "0757616511",
                                Mail = "mirceaand@gmail.com",
                                StatusCode = (int)PatientStatus.LoyalPatient,
                                Hidden = false
                            },
                            new Patient
                            {
                                FirstName = "Rotaru",
                                LastName = "Andreea",
                                PhoneNumber = "0757254785",
                                Mail = "androt@yahoo.com",
                                StatusCode = (int)PatientStatus.LoyalPatient,
                                Hidden = false
                            },
                            new Patient
                            {
                                FirstName = "Andreea",
                                LastName = "Ana",
                                PhoneNumber = "0755767254",
                                Mail = "andana@gmail.com",
                                StatusCode = (int)PatientStatus.LoyalPatient,
                                Hidden = true
                            });

                            for(int i = 0; i< 500;  i++)
                            {
                                context.Patients.AddRange(
                                    new Patient
                                    {
                                        FirstName = $"FirstName{i}",
                                        LastName = $"LastName{i}",
                                        PhoneNumber = GetRandomPhoneNumber(),
                                        Mail = $"e_mail_adress{i}@gmail.com",
                                        StatusCode = (int)PatientStatus.Patient,
                                        Hidden = false
                                    });
                            }

                            context.SaveChanges();
                        }

                        if (context.PatientSheets.Any() == false)
                        {
                            context.PatientSheets.AddRange(
                            new PatientSheet
                            {
                                PatientId = 1,
                                NationalIdentificationNumber = "1990713123123",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "antecedente de la parinti lipsa",
                                AntecedentsP = "acnee",
                                Town = "Suceava",
                                Street = "Calea Unirii numarul 12",
                                Gender = 1,
                                DateOfBirth = DateTime.Parse("1989-02-12")
                            },
                            new PatientSheet
                            {
                                PatientId = 2,
                                NationalIdentificationNumber = "1990713123321",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "",
                                AntecedentsP = "",
                                Town = "Suceava",
                                Street = "Calea Unirii numarul 21",
                                Gender = 1,
                                DateOfBirth = DateTime.Parse("1934-05-12")
                            },
                            new PatientSheet
                            {
                                PatientId = 3,
                                NationalIdentificationNumber = "1990713123098",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "",
                                AntecedentsP = "acnee",
                                Town = "Suceava",
                                Street = "Calea Unirii numarul 44",
                                Gender = 1,
                                DateOfBirth = DateTime.Parse("1929-02-12")
                            },
                            new PatientSheet
                            {
                                PatientId = 4,
                                NationalIdentificationNumber = "2990713123322",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "antecedente de la parinti lipsa",
                                AntecedentsP = "",
                                Town = "Plopeni",
                                Street = "Calea Unirii numarul 12",
                                Gender = 0,
                                DateOfBirth = DateTime.Parse("1989-03-12")
                            },
                            new PatientSheet
                            {
                                PatientId = 5,
                                NationalIdentificationNumber = "2990713123123",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "",
                                AntecedentsP = "acnee",
                                Town = "Falticeni",
                                Street = "Calea Unirii numarul 55",
                                Gender = 0,
                                DateOfBirth = DateTime.Parse("2000-07-24")
                            });

                            for (int i = 0; i < 500; i++)
                            {
                                ressult = random.Next(1, 3);
                                context.PatientSheets.AddRange(
                                    new PatientSheet
                                    {
                                        PatientId = i + 6,
                                        NationalIdentificationNumber = GetRandomCNP(),
                                        PhysicalExamination = ressult == 1 ? "Clinic sanatos" : "Pe moarte",
                                        AntecedentsH = $"Some information {i}",
                                        AntecedentsP = $"Another Informations {i}",
                                        Town = ressult == 1 ? "Suceava" : "Falticeni",
                                        Street = ressult == 1 ? $"Calea Unirii nr. {i}" : $"Calea Eroilor nr. {i}",
                                        Gender = random.Next(0, 2),
                                        DateOfBirth = DateTime.Now.AddYears(random.Next(1, 50))
                                    }) ;
                            }

                            context.SaveChanges();
                        }

                        if (context.Consultations.Any() == false)
                        {
                            context.Consultations.AddRange(
                            new Consultation
                            {
                                PatientSheetId = 1,
                                Prescriptions = "sample text Prescriptii 1",
                                Diagnostic = "sample text Diagnostic 1",
                                Symptoms = "sample text Simptome 1",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 1,
                                Prescriptions = "sample text Prescriptii 2",
                                Diagnostic = "sample text Diagnostic 2",
                                Symptoms = "sample text Simptome 2",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 1,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 2,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 3,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 4,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 5,
                                Prescriptions = "sample text Prescriptii 1",
                                Diagnostic = "sample text Diagnostic 1",
                                Symptoms = "sample text Simptome 1",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                PatientSheetId = 5,
                                Prescriptions = "sample text Prescriptii 2",
                                Diagnostic = "sample text Diagnostic 2",
                                Symptoms = "sample text Simptome 2",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            });

                            for (int i = 0; i < 900; i++)
                            {
                                ressult = random.Next(1, 3);
                                context.Consultations.AddRange(
                                    new Consultation
                                    {
                                        PatientSheetId = random.Next(1, 500),
                                        Prescriptions = $"sample text Prescriptii{i}",
                                        Diagnostic = $"sample text Diagnostic {i}",
                                        Symptoms = $"sample text Simptome {i}",
                                        CreationDate = DateTime.Now
                                    });
                            }
                            context.SaveChanges();
                        }

                        if (context.Appointments.Any() == false)
                        {
                            context.Appointments.AddRange(
                            new Appointment
                            {
                                PatientId = 1,
                                MedicId = 1,
                                RoomId = 1,
                                StartDate = DateTime.Now.AddDays(1).AddMinutes(15),
                                EndDate = DateTime.Now.AddDays(1).AddMinutes(75),
                                Procedure = "Anestezie",
                                Made = true,
                                ResponsibleForAppointment = "Administrator",
                                AppointmentCreationDate = DateTime.Now.AddDays(1),
                                AppointmentType = (int)AppointmentType.Private,
                                Hidden = false
                            },
                            new Appointment
                            {
                                PatientId = 4,
                                MedicId = 3,
                                RoomId = 5,
                                StartDate = DateTime.Now.AddDays(3).AddMinutes(15),
                                EndDate = DateTime.Now.AddDays(3).AddMinutes(75),
                                Procedure = "Apucuntura",
                                Made = true,
                                ResponsibleForAppointment = "Administrator",
                                AppointmentCreationDate = DateTime.Now.AddDays(3),
                                AppointmentType = (int)AppointmentType.Private,
                                Hidden = false
                            },
                            new Appointment
                            {
                                PatientId = 3,
                                MedicId = 2,
                                RoomId = 4,
                                StartDate = DateTime.Now.AddDays(2).AddMinutes(115),
                                EndDate = DateTime.Now.AddDays(2).AddMinutes(175),
                                Procedure = "Anestezie",
                                Made = true,
                                ResponsibleForAppointment = "Administrator",
                                AppointmentCreationDate = DateTime.Now.AddDays(2),
                                AppointmentType = (int)AppointmentType.Private,
                                Hidden = false
                            },
                            new Appointment
                            {
                                PatientId = 2,
                                MedicId = 1,
                                RoomId = 2,
                                StartDate = DateTime.Now.AddDays(7).AddMinutes(15),
                                EndDate = DateTime.Now.AddDays(7).AddMinutes(75),
                                Procedure = "Anestezie",
                                Made = true,
                                ResponsibleForAppointment = "Administrator",
                                AppointmentCreationDate = DateTime.Now.AddDays(7),
                                AppointmentType = (int)AppointmentType.Private,
                                Hidden = false
                            });

                            for (int i = 0; i < 1500; i++)
                            {
                                ressult = random.Next(1, 3);
                                startTime = GetRandomStartDate().AddMinutes(random.Next(30, 600));
                                context.Appointments.AddRange(
                                    new Appointment
                                    {
                                        PatientId = random.Next(1, 500),
                                        MedicId = random.Next(1, 4),
                                        RoomId = random.Next(1, 9),
                                        StartDate = startTime,
                                        EndDate = GetRandomEndDate(startTime),
                                        Procedure = $"Procedure test{i}",
                                        Made = true,
                                        ResponsibleForAppointment = "Administrator",
                                        AppointmentCreationDate = DateTime.Now.AddHours(random.Next(1,1000)),
                                        AppointmentType = ressult == 1 ? (int)AppointmentType.Private : (int)AppointmentType.Insurance,
                                        Hidden = false
                                    });
                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        //private static async  Task SeedClaimsForModeratorAdmin(RoleManager<IdentityRole> roleManager)
        //{
        //    var adminRole = await roleManager.FindByNameAsync("Moderator");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "Patients");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "PatientSheets");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "Consultations");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "Appointments");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "Rooms");
        //    await SeedData.AddPermissionClaim(roleManager, adminRole, "Medics");
        //}

        //public static async Task AddPermissionClaim(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        //{
        //    var allClaims = await roleManager.GetClaimsAsync(role);
        //    var allPermissions = Permissions.GeneratePermissionsForModule(module);
        //    foreach (var permission in allPermissions)
        //    {
        //        if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        //        {
        //            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        //        }
        //    }
        //}



        private static DateTime GetRandomStartDate()
        {
            var rand = new Random();
            DateTime date = DateTime.Now;
            return date.AddDays(rand.Next(1, 50));
        }
        private static DateTime GetRandomEndDate(DateTime startTime)
        {
            var rand = new Random();
            DateTime date = startTime;
            return date.AddMinutes(rand.Next(45, 120));
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
        private static string GetRandomPhoneNumber()
        {
            var rand = new Random();

            string number = $"0";
            for (int i = 1; i <= 9; i++)
            {
                number += $"{rand.Next(0, 9)}";
            }

            return number;
        }
    }
}
