using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.Domain
{
    public class Medic : BaseEntityDomain
    {
        [Required]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Mail { get; set; }

        public string Description { get; set; }

        public string Designation {  get; set; }
    }
}
