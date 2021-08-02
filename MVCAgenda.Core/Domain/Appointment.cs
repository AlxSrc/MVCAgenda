using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Appointment : BaseModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }


        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }


        [DisplayName("Camera")]
        public int RoomId { get; set; }


        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }


        [DisplayName("Medicul")]
        public int MedicId { get; set; }


        [ForeignKey("MedicId")]
        public virtual Medic Medic { get; set; }


        [DisplayName("Data consultatie")]
        [DataType(DataType.Date)]
        [Required]
        public string AppointmentDate { get; set; }


        [DisplayName("Ora consultatie")]
        [DataType(DataType.Time)]
        [Required]
        public string AppointmentHour { get; set; }


        [StringLength(75, MinimumLength = 1)]
        [DisplayName("Procedura")]
        [Required]
        public string Procedure { get; set; } = "neidentificat";


        [DisplayName("Efectuata")]
        [Required]
        public int Made { get; set; } = 1;


        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Programare realizata de")]
        [Required]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare consultatie")]
        [DataType(DataType.DateTime)]
        [Required]
        public string AppointmentCreationDate { get; set; }


        [DisplayName("Sters")]
        [Required]
        public int Visible { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}

/*
Tabelul Programare:
    Nume
    Prenume
    Numar de telefon
    Mail


    Data programare
    Ora ora programare
    Medic care efectueaza procedura
    Room
 unde se efectueaza procedura
    Procedura(Denumire)
    Efectuat(daca bifati ca o programare nu s-a efectuat
            se subintelege ca pacientul nu s-a prezentat
            si se trece pacientul la blacklist)
    _Cel ce efectueaza programarea
    _Data la care s-a facut programarea
    _vizibilitate

Medic

Camere:
*/