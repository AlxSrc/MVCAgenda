using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class RoomViewModel : BaseModel
    {
        [DisplayName("Denumire camera")]
        [Required]
        public string RoomName { get; set; }

        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
