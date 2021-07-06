using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Domain
{
    public class Medic
    {
        [Key] 
        public int MedicId { get; set; }


        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire medic")]
        [Required]
        public string DenumireMedic { get; set; }

        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
