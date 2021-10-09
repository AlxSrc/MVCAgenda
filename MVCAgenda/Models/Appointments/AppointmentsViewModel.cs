using MVCAgenda.Models.SyncfusionScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentsViewModel
    {
        public AppointmentsViewModel()
        {
            AppointmentsList = new List<AppointmentListItemViewModel>();
        }

        #region Param
        public List<AppointmentListItemViewModel> AppointmentsList { get; set; }

        public bool? Hidden { get; set; }
        public bool? Blacklist { get; set; }
        public bool? Daily { get; set; }
        #endregion

        #region Param for search

        [Display(Name = "Numele de familie")]
        public string SearchByName { get; set; }

        [Display(Name = "Numar de telefon")]
        public string SearchByPhoneNumber { get; set; }

        [Display(Name = "Adresa de e-mail")]
        public string SearchByEmail { get; set; }

        [Display(Name = "Data start programare")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? SearchByAppointmentStartDate { get; set; } = DateTime.Now;

        [Display(Name = "Data limita programare")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? SearchByAppointmentEndDate { get; set; } = DateTime.Now;

        [Display(Name = "Medic")]
        public int? SearchByMedic { get; set; }

        [Display(Name = "Camera")]
        public int? SearchByRoom { get; set; }

        [Display(Name = "Procedura")]
        public string SearchByProcedure { get; set; }
        #endregion
    }
}