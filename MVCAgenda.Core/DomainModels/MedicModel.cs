using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.DomainModels
{
    public class MedicModel
    {
        [Key] 
        public int MedicId { get; set; }


        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire medic")]
        [Required]
        public string MedicName { get; set; }


        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
