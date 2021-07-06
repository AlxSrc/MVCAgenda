using MVCAgenda.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class FisaPacientViewModel
    {
        [Key] public int FisaPacientId { get; set; }




        [DisplayName("Antecedente: heredo-colaterale")] 
        public string AntecedenteH { get; set; }
        [DisplayName("Antecedente: personale")] 
        public string AntecedenteP { get; set; }

        [DisplayName("Examen fizic")] 
        public string ExamenFizic { get; set; }

        public string CNP { get; set; }

        public string Sexul { get; set; }

        public string Localitatea { get; set; }

        public string Strada { get; set; }



        [DisplayName("Data nasterii")] 
        public string DataNasterii { get; set; }

        public List<Consultatie> Consultatii { get; set; }
    }
}
