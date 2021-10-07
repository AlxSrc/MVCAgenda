using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Core.Domain;
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
        public static async void Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var init = false;
                using (var context = new AgendaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AgendaContext>>()))
                {
                    if (init == true)
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
                                Name = "Doctor Ana-Maria",
                                Mail = "ana_maria@yahoo.com",
                                ImagePath = "",
                                Hidden = false
                            },
                            new Medic
                            {
                                Name = "Asistent Andrei",
                                Mail = "andrei@yahoo.com",
                                ImagePath = "",
                                Hidden = false
                            },
                            new Medic
                            {
                                Name = "Asistent Ionela",
                                Mail = "ionela@yahoo.com",
                                ImagePath = "",
                                Hidden = false
                            });

                            context.SaveChanges();
                        }

                        if (context.PatientsSheet.Any() == false)
                        {
                            context.PatientsSheet.AddRange(
                            new PatientSheet
                            {
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
                                NationalIdentificationNumber = "2990713123123",
                                PhysicalExamination = "Clinic sanatos",
                                AntecedentsH = "",
                                AntecedentsP = "acnee",
                                Town = "Falticeni",
                                Street = "Calea Unirii numarul 55",
                                Gender = 0,
                                DateOfBirth = DateTime.Parse("2000-07-24")
                            });

                            context.SaveChanges();
                        }

                        if (context.Consultations.Any() == false)
                        {
                            context.Consultations.AddRange(
                            new Consultation
                            {
                                SheetPatientId = 1,
                                Prescriptions = "sample text Prescriptii 1",
                                Diagnostic = "sample text Diagnostic 1",
                                Symptoms = "sample text Simptome 1",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 1,
                                Prescriptions = "sample text Prescriptii 2",
                                Diagnostic = "sample text Diagnostic 2",
                                Symptoms = "sample text Simptome 2",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 1,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 2,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 3,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 4,
                                Prescriptions = "sample text Prescriptii 3",
                                Diagnostic = "sample text Diagnostic 3",
                                Symptoms = "sample text Simptome 3",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 5,
                                Prescriptions = "sample text Prescriptii 1",
                                Diagnostic = "sample text Diagnostic 1",
                                Symptoms = "sample text Simptome 1",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            },
                            new Consultation
                            {
                                SheetPatientId = 5,
                                Prescriptions = "sample text Prescriptii 2",
                                Diagnostic = "sample text Diagnostic 2",
                                Symptoms = "sample text Simptome 2",
                                CreationDate = DateTime.Parse("2021-07-24 12:00")
                            });

                            context.SaveChanges();
                        }

                        if (context.Patients.Any() == false)
                        {
                            context.Patients.AddRange(
                            new Patient
                            {
                                PatientSheetId = 1,
                                FirstName = "Serediuc",
                                LastName = "Alexandru",
                                PhoneNumber = "0757541521",
                                Mail = "",
                                Blacklist = false,
                                Hidden = false
                            },
                            new Patient
                            {
                                PatientSheetId = 2,
                                FirstName = "Serediuc",
                                LastName = "Constantin",
                                PhoneNumber = "0741241712",
                                Mail = "Alex.gsa@yahoo.com",
                                Blacklist = false,
                                Hidden = false
                            },
                            new Patient
                            {
                                PatientSheetId = 3,
                                FirstName = "Mircea",
                                LastName = "Andrei",
                                PhoneNumber = "0757616511",
                                Mail = "mirceaand@gmail.com",
                                Blacklist = false,
                                Hidden = false
                            },
                            new Patient
                            {
                                PatientSheetId = 4,
                                FirstName = "Rotaru",
                                LastName = "Andreea",
                                PhoneNumber = "0757254785",
                                Mail = "androt@yahoo.com",
                                Blacklist = false,
                                Hidden = false
                            },
                            new Patient
                            {
                                PatientSheetId = 5,
                                FirstName = "Andreea",
                                LastName = "Ana",
                                PhoneNumber = "0755767254",
                                Mail = "andana@gmail.com",
                                Blacklist = false,
                                Hidden = true
                            });

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
                                Hidden = false
                            });

                            context.SaveChanges();
                        }

                        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.Doctor.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.Nurse.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.Receptionist.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.PersonalTrainer.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.Kinetotherapist.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

                        var moderatorUser = new IdentityUser
                        {
                            UserName = "moderator_agenda@gmail.com",
                            Email = "moderator_agenda@gmail.com",
                            EmailConfirmed = true
                        };

                        await userManager.CreateAsync(moderatorUser, "{Al@ka#9A#s&KA|");
                        await userManager.AddToRoleAsync(moderatorUser, Roles.Admin.ToString());

                        await SeedClaimsForModeratorAdmin(roleManager);
                    }
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private static async  Task SeedClaimsForModeratorAdmin(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Moderator");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "Patients");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "PatientSheets");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "Consultations");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "Appointments");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "Rooms");
            await SeedData.AddPermissionClaim(roleManager, adminRole, "Medics");
        }

        public static async Task AddPermissionClaim(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
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
