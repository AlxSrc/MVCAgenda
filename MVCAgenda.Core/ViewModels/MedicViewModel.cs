using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class MedicViewModel : BaseEntityModel
    {
        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire medic")]
        [Required]
        public string MedicName { get; set; }
    }
}
