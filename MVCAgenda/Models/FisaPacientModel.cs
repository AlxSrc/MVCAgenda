using MVCAgenda.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class FisaPacientModel
    {
        [Key] 
        public int FisaPacientId { get; set; }

        [DisplayName("Antecedente: heredo-colaterale")] 
        public string AntecedenteH { get; set; }
        [DisplayName("Antecedente: Personale")] 
        public string AntecedenteP { get; set; }

        public string ExamenFizic { get; set; }

        public string CNP { get; set; }

        public string Sexul { get; set; }

        public string Localitatea { get; set; }

        public string Strada { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Data nasterii")] 
        public DateTime DataNasterii { get; set; }
    }
}
