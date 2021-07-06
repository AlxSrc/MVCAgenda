using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Domain
{
    public class Pacient
    {
        [Key] 
        public int PacientId { get; set; }


        [DisplayName("Detalii Pacient")] 
        public int FisaPacientId { get; set; }

        [ForeignKey("FisaPacientId")] 
        public virtual FisaPacient FisaPacient { get; set; }

        public virtual Programare Programare { get; set; }



        [StringLength(60, MinimumLength = 1)]
        [Required] 
        public string Nume { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [Required] 
        public string Prenume { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Numar de telefon")]
        [Required] 
        public string NrDeTelefon { get; set; }

        [StringLength(60, MinimumLength = 0)]
        [DisplayName("Mail")] 
        public string Mail { get; set; }

        [Required] 
        public int Blacklist { get; set; }

        [Required] 
        public int Visible { get; set; } = 1;
    }
}
