using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Domain
{
    public class Camera
    {
        [Key] 
        public int CameraId { get; set; }

        [StringLength(25, MinimumLength = 1)]
        [DisplayName("Denumire camera")]
        [Required]
        public string DenumireCamera { get; set; }

        [DisplayName("Sters")]
        public int Visible { get; set; } = 1;
    }
}
