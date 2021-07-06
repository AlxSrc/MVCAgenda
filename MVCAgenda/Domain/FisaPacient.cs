using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Domain
{
    public class FisaPacient
    {
        [Key]
        public int FisaPacientId { get; set; }
        public virtual Pacient Persoana { get; set; }



        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Antecedente: heredo-colaterale")] 
        public string AntecedenteH { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Antecedente: Personale")] 
        public string AntecedenteP { get; set; }


        [StringLength(20, MinimumLength = 1)] 
        public string CNP { get; set; }

        
        public int Sexul { get; set; }


        [StringLength(60, MinimumLength = 1)] 
        public string Localitatea { get; set; }


        [StringLength(60, MinimumLength = 1)] 
        public string Strada { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Examen fizic")] 
        public string ExamenFizic { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Data nasterii")] 
        public DateTime DataNasterii { get; set; }
    }
}
