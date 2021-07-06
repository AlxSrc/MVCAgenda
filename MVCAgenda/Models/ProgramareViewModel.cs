using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class ProgramareViewModel
    {
        [Key] public int ProgramareId { get; set; }

        [DisplayName("Pacient")] 
        public int PacientId { get; set; }


        [Required]
        public int MedicId { get; set; }

        [Required]
        public int CameraId { get; set; }

        [Required]
        [DisplayName("Data consultatie")] 
        public string DataConsultatie { get; set; }

        [Required]
        [DisplayName("Ora consultatie")] 
        public string OraConsultatie { get; set; }

        [Required]
        public string Procedura { get; set; }

        [Required]
        public string Efectuata { get; set; }

        [DisplayName("Programare realizata de")]
        public string ResponsabilProgramare { get; set; }

        [DisplayName("Data creeare consultatie")]
        public string DataCreeareConsultatie { get; set; }

        public bool Visible { get; set; } = true;
    }
}
