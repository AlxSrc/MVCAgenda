using MVCAgenda.Core.Status;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Patient : BaseSoftDeleteEntity
    {
        public virtual PatientSheet SheetPatient { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }


        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string PhoneNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        public string Mail { get; set; }


        public int StatusCode { get; set; }
    }
}