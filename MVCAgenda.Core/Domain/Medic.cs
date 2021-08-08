using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.Domain
{
    public class Medic : BaseEntityModel
    {
        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire medic")]
        [Required]
        public string MedicName { get; set; }
    }
}
