using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Rooms
{
    public class RoomViewModel : BaseModel
    {
        [DisplayName("Denumire cameră")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Culoare principala")]
        public string PrimaryColor { get; set; }

        [DisplayName("Culoare secundara")]
        public string SecondaryColor { get; set; }

        [DisplayName("Descrierea")]
        public string Description { get; set; }
    }
}