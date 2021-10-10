using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.Domain
{
    public class Room : BaseEntityDomain
    {
        [StringLength(25, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string Description { get; set; }
    }
}