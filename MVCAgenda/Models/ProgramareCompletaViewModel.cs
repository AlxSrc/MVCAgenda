using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class ProgramareCompletaViewModel
    {
        [Key] 
        public int ProgramareId { get; set; }

        [DisplayName("Pacient")]
        public int PacientId { get; set; }


        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required]
        [DisplayName("Numar de telefon")] 
        public string NrDeTelefon { get; set; }

        [DisplayName("Mail")] 
        public string Mail { get; set; }

        public string Blacklist { get; set; }




        [Required]
        public int MedicId { get; set; }

        [Required]
        public int CameraId { get; set; }

        [DisplayName("Data consultatie")]
        [DataType(DataType.Date)]
        [Required] 
        public string DataConsultatie { get; set; }

        [DisplayName("Ora consultatie")]
        [DataType(DataType.Time)]
        [Required]
        public string OraConsultatie { get; set; }

        [Required]
        public string Procedura { get; set; }

        public int Efectuata { get; set; } = 1;
        public string EfectuataText { get; set; }

        [DisplayName("Motivul neefectuarii")]
        public string MotivulNeefectuarii { get; set; }


        [DisplayName("Programare realizata de")]
        public string ResponsabilProgramare { get; set; }


        [DisplayName("Data creeare consultatie")]
        [DataType(DataType.DateTime)] 
        public string DataCreeareConsultatie { get; set; }

        public bool Visible { get; set; } = true;
    }
}
