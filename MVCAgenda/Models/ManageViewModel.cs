using MVCAgenda.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models
{
    public class ManageViewModel
    {
        public List<Camera> Camere { get; set; }
        public List<Medic> Medici { get; set; }
    }
}
