using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core.Logging
{
    public enum LogTable
    {
        /// <summary>
        /// Patients
        /// </summary>
        Patients = 10,

        /// <summary>
        /// PatientSheets
        /// </summary>
        PatientSheets = 20,

        /// <summary>
        /// Consultations
        /// </summary>
        Consultations = 30,

        /// <summary>
        /// Appointments
        /// </summary>
        Appointments = 40,

        /// <summary>
        /// Rooms
        /// </summary>
        Rooms = 50,

        /// <summary>
        /// Medics
        /// </summary>
        Medics = 60
    }
}