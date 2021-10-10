using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models.BaseModels
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool Hidden { get; set; }
    }
}