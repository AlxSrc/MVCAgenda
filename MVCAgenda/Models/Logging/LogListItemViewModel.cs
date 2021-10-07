using MVCAgenda.Core.Logging;
using MVCAgenda.Models.BaseModels;
using System;
using System.ComponentModel;

namespace MVCAgenda.Models.Logging
{
    public partial class LogListItemViewModel : BaseEntityModel
    {
        [DisplayName("Action")]
        public string ShortMessage { get; set; }


        [DisplayName("Description")]
        public string FullMessage { get; set; }


        [DisplayName("Ip Adress")]
        public string IpAddress { get; set; }


        [DisplayName("Time")]
        public DateTime CreatedOnUtc { get; set; }


        [DisplayName("Type")]
        public string LogLevel { get; set; }
    }
}
