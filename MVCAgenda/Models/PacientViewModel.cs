using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class PacientViewModel
    {
        public int PacientId { get; set; }
        public int FisaPacientId { get; set; }


        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [DisplayName("Numar de telefon")]
        [Required]
        public string NrDeTelefon { get; set; }

        [DisplayName("Mail")]
        public string Mail { get; set; }

        public string Blacklist { get; set; }

        public bool Visible { get; set; }
    }
}
