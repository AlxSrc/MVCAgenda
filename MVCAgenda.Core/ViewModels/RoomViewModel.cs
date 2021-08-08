using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class RoomViewModel : BaseEntityModel
    {
        [DisplayName("Denumire camera")]
        [Required]
        public string RoomName { get; set; }
    }
}
