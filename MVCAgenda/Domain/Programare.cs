using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Domain
{
    public class Programare
    {
        [Key] 
        public int ProgramareId { get; set; }


        [DisplayName("Pacient")]
        public int PacientId { get; set; } = 1;
        [ForeignKey("PacientId")]
        public virtual Pacient Persoana { get; set; }

        [DisplayName("Camera")]
        public int CameraId { get; set; } = 1;
        [ForeignKey("CameraId")]
        public virtual Camera Camera { get; set; }

        [DisplayName("Medicul")]
        public int MedicId { get; set; } = 1;
        [ForeignKey("MedicId")]
        public virtual Medic Medic { get; set; }

        [DisplayName("Data consultatie")]
        [DataType(DataType.Date)]
        [Required] 
        public string DataConsultatie { get; set; }

        [DisplayName("Ora consultatie")]
        [DataType(DataType.Time)]
        [Required] 
        public string OraConsultatie { get; set; }

        [StringLength(75, MinimumLength = 1)]
        [Required] 
        public string Procedura { get; set; } = "neidentificat";

        [Required] 
        public int Efectuata { get; set; } = 1;

        [DisplayName("Motivul neefectuarii")]
        [StringLength(100, MinimumLength = 0)] 
        public string MotivNeefectuata { get; set; }
        
        [DisplayName("Programare realizata de")]
        [StringLength(30, MinimumLength = 1)]
        [Required] 
        public string ResponsabilProgramare { get; set; }


        [DisplayName("Data creeare consultatie")]
        [DataType(DataType.DateTime)]
        [Required] 
        public string DataCreeareConsultatie { get; set; }

        [Required] 
        public int Visible { get; set; }
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
    Camera unde se efectueaza procedura
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