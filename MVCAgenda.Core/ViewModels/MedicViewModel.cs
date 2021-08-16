using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class MedicViewModel : BaseEntityModel
    {
        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Numele medicului")]
        [Required]
        public string MedicName { get; set; }
    }
}
