using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.DomainModels
{
    public class RoomModel
    {
        [Key]
        public int RoomId { get; set; }

        [DisplayName("Denumire camera")]
        [Required]
        public string RoomName { get; set; }

        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
