using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Data;
using MVCAgenda.Domain;
using System;
using System.Linq;

namespace MVCAgenda.Data
{
    public class SeedData
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public static void Initialize(IServiceProvider serviceProvider)
        {

            var rand = new Random();
            using (var context = new AgendaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AgendaContext>>()))
            {
                if (context.Camera.Any() == false)
                {   
                    context.Camera.AddRange(
                    new Camera
                    {
                        DenumireCamera = "Camera 1 medical"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 2 medical"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 3 medical"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 1 corporal"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 2 corporal"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 3 corporal"
                    }, new Camera
                    {
                        DenumireCamera = "Camera 4 corporal"
                    }, new Camera
                    {
                        DenumireCamera = "Sala de sport"
                    });
                }
                context.SaveChanges();

                if (context.Medic.Any() == false)
                {
                    context.Medic.AddRange(
                    new Medic
                    {
                        DenumireMedic = "Doctor Ana-Maria"
                    }, new Medic
                    {
                        DenumireMedic = "Asistent Andrei"
                    }, new Medic
                    {
                        DenumireMedic = "Asistent Ionela"
                    });
                }
                context.SaveChanges();

                if (context.Consultatie.Any() == false)
                {
                    context.Consultatie.AddRange(
                    new Consultatie
                    {
                        FisaPacientId = 1,
                        Prescriptii= "sample text Prescriptii 1",
                        Diagnostic = "sample text Diagnostic 1",
                        Simptome = "sample text Simptome 1",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 1,
                        Prescriptii = "sample text Prescriptii 2",
                        Diagnostic = "sample text Diagnostic 2",
                        Simptome = "sample text Simptome 2",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 1,
                        Prescriptii = "sample text Prescriptii 3",
                        Diagnostic = "sample text Diagnostic 3",
                        Simptome = "sample text Simptome 3",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 2,
                        Prescriptii = "sample text Prescriptii 3",
                        Diagnostic = "sample text Diagnostic 3",
                        Simptome = "sample text Simptome 3",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 3,
                        Prescriptii = "sample text Prescriptii 3",
                        Diagnostic = "sample text Diagnostic 3",
                        Simptome = "sample text Simptome 3",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 4,
                        Prescriptii = "sample text Prescriptii 3",
                        Diagnostic = "sample text Diagnostic 3",
                        Simptome = "sample text Simptome 3",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 5 ,
                        Prescriptii = "sample text Prescriptii 1",
                        Diagnostic = "sample text Diagnostic 1",
                        Simptome = "sample text Simptome 1",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    }, new Consultatie
                    {
                        FisaPacientId = 5,
                        Prescriptii = "sample text Prescriptii 2",
                        Diagnostic = "sample text Diagnostic 2",
                        Simptome = "sample text Simptome 2",
                        DataCreeare = DateTime.Parse("2000-07-24")
                    });
                    context.SaveChanges();

                    string date;
                    for (int i = 1; i < 600; i++)
                    {
                        date = GetRandomDate();
                        context.Consultatie.AddRange(
                            new Consultatie
                            {
                                FisaPacientId = i,
                                Prescriptii = "sample text Prescriptii 2",
                                Diagnostic = "sample text Diagnostic 2",
                                Simptome = "sample text Simptome 2",
                                DataCreeare = DateTime.Parse(date)
                            });
                    }
                }

                if (context.FisaPacient.Any() == false)
                {
                    context.FisaPacient.AddRange(
                    new FisaPacient
                    {
                        CNP = "1990713123123",
                        ExamenFizic = "Clinic sanatos",
                        AntecedenteH = "antecedente de la parinti lipsa",
                        AntecedenteP = "acnee",
                        Localitatea= "Suceava",
                        Strada = "Calea Unirii numarul 12",
                        Sexul = 1,
                        DataNasterii = DateTime.Parse("1989-02-12")
                    },
                    new FisaPacient
                    {
                        CNP = "1990713123321",
                        ExamenFizic = "Clinic sanatos",
                        AntecedenteH = "",
                        AntecedenteP = "",
                        Localitatea = "Suceava",
                        Strada = "Calea Unirii numarul 21",
                        Sexul = 1,
                        DataNasterii = DateTime.Parse("1934-05-12")
                    },
                    new FisaPacient
                    {
                        CNP = "1990713123098",
                        ExamenFizic = "Clinic sanatos",
                        AntecedenteH = "",
                        AntecedenteP = "acnee",
                        Localitatea = "Suceava",
                        Strada = "Calea Unirii numarul 44",
                        Sexul = 1,
                        DataNasterii = DateTime.Parse("1929-02-12")
                    },
                    new FisaPacient
                    {
                        CNP = "2990713123322",
                        ExamenFizic = "Clinic sanatos",
                        AntecedenteH = "antecedente de la parinti lipsa",
                        AntecedenteP = "",
                        Localitatea = "Plopeni",
                        Strada = "Calea Unirii numarul 12",
                        Sexul = 0,
                        DataNasterii = DateTime.Parse("1989-03-12")
                    },
                    new FisaPacient
                    {
                        CNP = "2990713123123",
                        ExamenFizic = "Clinic sanatos",
                        AntecedenteH = "",
                        AntecedenteP = "acnee",
                        Localitatea = "Falticeni",
                        Strada = "Calea Unirii numarul 55",
                        Sexul = 0,
                        DataNasterii = DateTime.Parse("2000-07-24")
                    });

                    for (int i = 1; i < 400; i++)
                    {
                        context.FisaPacient.AddRange(
                            new FisaPacient
                            {
                                CNP = GetRandomCNP(),
                                ExamenFizic = "Clinic sanatos",
                                AntecedenteH = "sample text",
                                AntecedenteP = "acnee",
                                Localitatea = "Falticeni",
                                Strada = "Calea Unirii numarul 55",
                                Sexul = rand.Next(0, 1),
                                DataNasterii = DateTime.Parse(GetRandomDate())
                            });
                    }
                }

                if (context.Pacient.Any() == false)
                {
                    context.Pacient.AddRange(
                    new Pacient
                    {
                        FisaPacientId = 1,
                        Nume = "Serediuc",
                        Prenume = "Alexandru",
                        NrDeTelefon = "0757541521",
                        Mail = "",
                        Blacklist = 1,
                        Visible = 1
                    },
                    new Pacient
                    {
                        FisaPacientId = 2,
                        Nume = "Serediuc",
                        Prenume = "Constantin",
                        NrDeTelefon = "0741241712",
                        Mail = "Alex.gsa@yahoo.com",
                        Blacklist = 0,
                        Visible = 1
                    },
                    new Pacient
                    {
                        FisaPacientId = 3,
                        Nume = "Mircea",
                        Prenume = "Andrei",
                        NrDeTelefon = "0757616511",
                        Mail = "mirceaand@gmail.com",
                        Blacklist = 0,
                        Visible = 1
                    },
                    new Pacient
                    {
                        FisaPacientId = 4,
                        Nume = "Rotaru",
                        Prenume = "Andreea",
                        NrDeTelefon = "0757254785",
                        Mail = "androt@yahoo.com",
                        Blacklist = 0,
                        Visible = 1
                    },
                    new Pacient
                    {
                        FisaPacientId = 5,
                        Nume = "Andreea",
                        Prenume = "Ana",
                        NrDeTelefon = "0755767254",
                        Mail = "andana@gmail.com",
                        Blacklist = 0,
                        Visible = 1
                    });

                    for (int i = 6; i < 400; i++)
                    {
                        context.Pacient.AddRange(
                            new Pacient
                            {
                                FisaPacientId = i,
                                Nume = $"Test{i}",
                                Prenume = $"PreTest{i}",
                                NrDeTelefon = GetRandomNumber(),
                                Mail = $"Test{i}@mail.com",
                                Blacklist = 0,
                                Visible = 1
                            });
                    }
                }

                if (context.Programare.Any() == false)
                {
                    context.Programare.AddRange(
                    new Programare
                    {
                        PacientId = 1,
                        MedicId = 1,
                        CameraId = 1,
                        DataConsultatie = "2021-02-12",
                        OraConsultatie = "12-00",
                        Procedura = "Anestezie",
                        Efectuata = 1,
                        ResponsabilProgramare = "Administrator",
                        DataCreeareConsultatie = "2021-04-14T09:35",
                        Visible = 1
                    },
                    new Programare
                    {
                        PacientId = 4,
                        MedicId = 3,
                        CameraId = 5,
                        DataConsultatie = "2021-02-12",
                        OraConsultatie = "13-00",
                        Procedura = "Apucuntura",
                        Efectuata = 1,
                        ResponsabilProgramare = "Administrator",
                        DataCreeareConsultatie = "2021-04-14T09:35",
                        Visible = 1
                    },
                    new Programare
                    {
                        PacientId = 3,
                        MedicId = 2,
                        CameraId = 4,
                        DataConsultatie = "2021-03-12",
                        OraConsultatie = "14-30",
                        Procedura = "Anestezie",
                        Efectuata = 1,
                        ResponsabilProgramare = "Administrator",
                        DataCreeareConsultatie = "2021-04-14T09:35",
                        Visible = 1
                    },
                    new Programare
                    {
                        PacientId = 2,
                        MedicId = 1,
                        CameraId = 2,
                        DataConsultatie = "2021-02-02",
                        OraConsultatie = "12-00",
                        Procedura = "Anestezie",
                        Efectuata = 1,
                        ResponsabilProgramare = "Administrator",
                        DataCreeareConsultatie = "2021-04-14T09:35",
                        Visible = 1
                    });

                    context.SaveChanges();
                    for (int i = 6; i < 1600; i++)
                    {
                        context.Programare.AddRange(
                        new Programare
                        {
                            PacientId = rand.Next(1,399),
                            MedicId = rand.Next(1, 4),
                            CameraId = rand.Next(1, 8),
                            DataConsultatie = GetRandomDate(),
                            OraConsultatie = GetRandomOra(),
                            Procedura = $"Sample text{i}",
                            Efectuata = 1,
                            ResponsabilProgramare = "Administrator",
                            DataCreeareConsultatie = GetRandomFullDate(),
                            Visible = 1
                        });
                    }

                }
                try
                {
                    context.SaveChanges();
                }catch(Exception ex)
                {
                    var msg = ex.Message;
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
            string[] camere = new string[] { "Camera 1 Corporal", "Camera 2 Corporal", "Camera 3 Corporal", "Camera 1 medical", "Camera 2 medical", "Camera 3 medical", "Camera 4 medical", "Sala sport" };

            string camera = camere[rand.Next(0, 7)];

            return camera;
        }
    }
}
