using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Medics
{
    public class MedicViewModel : BaseModel
    {
        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Numele medicului")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Adresa de e-mail")]
        public string Mail { get; set; }

        [DisplayName("Imagine")]
        public string ImagePath { get; set; }

        [DisplayName("Descrierea")]
        public string Description { get; set; }

        [DisplayName("Ocupatia")]
        public string Designation { get; set; }
    }
}