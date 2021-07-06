using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Domain
{
    public class Consultatie
    {
        [Key]
        public int ConsultatieId { get; set; }


        [DisplayName("Fisa Pacient")] 
        public int FisaPacientId { get; set; }

        [ForeignKey("PacientId")] 
        public virtual FisaPacient FisaPacient { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Data creeare")] 
        public DateTime DataCreeare { get; set; }

        [StringLength(450, MinimumLength = 1)] 
        public string Simptome { get; set; }

        [StringLength(450, MinimumLength = 1)] 
        public string Diagnostic { get; set; }

        [StringLength(450, MinimumLength = 1)] 
        public string Prescriptii { get; set; }
    }
}
