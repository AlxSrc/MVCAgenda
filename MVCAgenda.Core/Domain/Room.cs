using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.Domain
{
    public class Room : BaseModel
    {
        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire camera")]
        [Required]
        public string RoomName { get; set; }

        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
