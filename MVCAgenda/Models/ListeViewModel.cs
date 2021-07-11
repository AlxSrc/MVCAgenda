using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCAgenda.Models
{
    public class ListeViewModel
    {
        public List<PacientViewModel> Pacienti { get; set; }

        public List<ProgramareViewModel> Programari { get; set; }

        public List<AfisareProgramareViewModel> ProgramariComplete { get; set; }


        public string SearchStringNume { get; set; }

        public string SearchStringNumarDeTelefon { get; set; }

        public string SearchStringEmail { get; set; }

        public string SearchStringOra { get; set; }

        public string SearchStringData { get; set; }

        public int SearchMedic { get; set; }
        public int SearchCamera { get; set; }

        public ListeViewModel()
        {
            Pacienti = new List<PacientViewModel>();

            Programari = new List<ProgramareViewModel>();

            ProgramariComplete = new List<AfisareProgramareViewModel>();
        }

    }
}
