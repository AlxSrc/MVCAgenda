using MVCAgenda.Domain;
using MVCAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Factories
{
    public class AgendaFactory
    {
        public static PacientViewModel PreparePacientViewModel(Pacient pacient)
        {
            var pacientViewModel = new PacientViewModel
            {
                PacientId = pacient.PacientId,
                FisaPacientId = pacient.FisaPacientId,
                Nume = pacient.Nume,
                Prenume = pacient.Prenume,
                NrDeTelefon = pacient.NrDeTelefon,
                Mail = pacient.Mail
            };

            if (pacient.Blacklist == 1)
            {
                pacientViewModel.Blacklist = "<span class=\"badge bg-danger\">Da</span>";
            }
            else if (pacient.Blacklist == 0)
            {
                pacientViewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                pacientViewModel.Blacklist = "-";
            }

            if (pacient.Visible >= 1)
            {
                pacientViewModel.Visible = true;
            }
            else if (pacient.Visible == 0)
            {
                pacientViewModel.Visible = false;
            }

            return pacientViewModel;
        }

        public static ProgramareViewModel PrepareProgramareViewModel(Programare programare)
        {
            var programareViewModel = new ProgramareViewModel
            {
                ProgramareId = programare.ProgramareId,
                PacientId = programare.PacientId,
                MedicId = programare.MedicId,
                CameraId = programare.MedicId,
                DataConsultatie = programare.DataConsultatie.ToString(),
                OraConsultatie = programare.OraConsultatie.ToString(),
                Procedura = programare.Procedura,
                ResponsabilProgramare = programare.ResponsabilProgramare,
                DataCreeareConsultatie = programare.DataCreeareConsultatie
            };

            if (programare.Efectuata == 1)
            {
                programareViewModel.Efectuata = "<span class=\"badge bg-success\">Da</span>";
            }
            else if (programare.Efectuata == 0)
            {
                programareViewModel.Efectuata = "<span class=\"badge bg-Danger\">Nu</span>";
            }
            else
            {
                programareViewModel.Efectuata = "-";
            }

            if (programare.Visible >= 1)
            {
                programareViewModel.Visible = true;
            }
            else if (programare.Visible == 0)
            {
                programareViewModel.Visible = false;
            }

            return programareViewModel;
        }

        public static ProgramareCompletaViewModel PrepareProgramareCompletaViewModel(Programare programare, Pacient pacient)
        {
            var programareCompletaViewModel = new ProgramareCompletaViewModel
            {
                ProgramareId = programare.ProgramareId,
                PacientId = programare.PacientId,
                Nume = pacient.Nume,
                Prenume = pacient.Prenume,
                NrDeTelefon = pacient.NrDeTelefon,
                Mail = pacient.Mail,
                MedicId = programare.MedicId,
                CameraId = programare.CameraId,
                DataConsultatie = programare.DataConsultatie.ToString(),
                OraConsultatie = programare.OraConsultatie.ToString(),
                Procedura = programare.Procedura,
                ResponsabilProgramare = programare.ResponsabilProgramare,
                DataCreeareConsultatie = programare.DataCreeareConsultatie
            };

            if (programare.Efectuata == 1)
            {
                programareCompletaViewModel.EfectuataText = "<span class=\"badge bg-success\">Da</span>";
            }
            else if (programare.Efectuata == 0)
            {
                programareCompletaViewModel.EfectuataText = "<span class=\"badge bg-danger\">Nu</span>";
            }
            else
            {
                programareCompletaViewModel.EfectuataText = "-";
            }

            if (pacient.Blacklist == 1)
            {
                programareCompletaViewModel.Blacklist = "<span class=\"badge bg-Danger\">Da</span>";
            }
            else if (pacient.Blacklist == 0)
            {
                programareCompletaViewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                programareCompletaViewModel.Blacklist = "-";
            }

            if (programare.Visible >= 1)
            {
                programareCompletaViewModel.Visible = true;
            }
            else if (programare.Visible == 0)
            {
                programareCompletaViewModel.Visible = false;
            }

            return programareCompletaViewModel;
        }
        public static AfisareProgramareViewModel PrepareAfisareProgramareViewModel(Programare programare, Pacient pacient, Camera camera, Medic medic)
        {
            var programareCompletaViewModel = new AfisareProgramareViewModel
            {
                ProgramareId = programare.ProgramareId,
                PacientId = programare.PacientId,
                Nume = pacient.Nume,
                Prenume = pacient.Prenume,
                NrDeTelefon = pacient.NrDeTelefon,
                Mail = pacient.Mail,
                Medic = medic.DenumireMedic,
                Camera = camera.DenumireCamera,
                DataConsultatie = programare.DataConsultatie.ToString(),
                OraConsultatie = programare.OraConsultatie.ToString(),
                Procedura = programare.Procedura,
                ResponsabilProgramare = programare.ResponsabilProgramare,
                DataCreeareConsultatie = programare.DataCreeareConsultatie
            };

            if (programare.Efectuata == 1)
            {
                programareCompletaViewModel.EfectuataText = "<span class=\"badge bg-success\">Da</span>";
            }
            else if (programare.Efectuata == 0)
            {
                programareCompletaViewModel.EfectuataText = "<span class=\"badge bg-danger\">Nu</span>";
            }
            else
            {
                programareCompletaViewModel.EfectuataText = "-";
            }

            if (pacient.Blacklist == 1)
            {
                programareCompletaViewModel.Blacklist = "<span class=\"badge bg-Danger\">Da</span>";
            }
            else if (pacient.Blacklist == 0)
            {
                programareCompletaViewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                programareCompletaViewModel.Blacklist = "-";
            }

            if (programare.Visible >= 1)
            {
                programareCompletaViewModel.Visible = true;
            }
            else if (programare.Visible == 0)
            {
                programareCompletaViewModel.Visible = false;
            }

            return programareCompletaViewModel;
        }
        public static FisaPacientViewModel PrepareFisaPacientViewModel(FisaPacient fisaPacient, List<Consultatie> consultatii)
        {
            //List<Consultatie> consultatiiModel = new List<Consultatie>();
            var fisaPacientViewModel = new FisaPacientViewModel
            {
                FisaPacientId = fisaPacient.FisaPacientId,
                AntecedenteH = fisaPacient.AntecedenteH,
                AntecedenteP = fisaPacient.AntecedenteP,
                ExamenFizic = fisaPacient.ExamenFizic,
                CNP = fisaPacient.CNP,
                DataNasterii = fisaPacient.DataNasterii.ToString("yyyy-MM-dd"),
                Localitatea = fisaPacient.Localitatea,
                Strada = fisaPacient.Strada
            };

            if (fisaPacient.Sexul == 1)
            {
                fisaPacientViewModel.Sexul = "Masculin";
            }
            else if (fisaPacient.Sexul == 0)
            {
                fisaPacientViewModel.Sexul = "Feminin";
            }
            else
            {
                fisaPacientViewModel.Sexul = "-";
            }

            //foreach (var consultatie in consultatii)
            //    if (fisaPacient.FisaPacientId == consultatie.FisaPacientId)
            //        consultatiiModel.Add(consultatie);
            //
            //fisaPacientViewModel.Consultatii = consultatiiModel;

            fisaPacientViewModel.Consultatii = consultatii;

            return fisaPacientViewModel;
        }
    }
}
