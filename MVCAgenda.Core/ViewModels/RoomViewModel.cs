using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class RoomViewModel : BaseEntityModel
    {
        [DisplayName("Denumire cameră")]
        [Required]
        public string RoomName { get; set; }
    }
}
