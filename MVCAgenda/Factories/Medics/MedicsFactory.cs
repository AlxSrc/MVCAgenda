using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Medics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Medics
{
    public class MedicsFactory: IMedicsFactory
    {
        public async Task<MedicViewModel> PrepereMedicViewModel(Medic medic)
        {
            return new MedicViewModel()
            {
                Id = medic.Id,
                Name = medic.Name,
                Mail = medic.Mail,
                ImagePath = medic.ImagePath,
                Designation = medic.Designation,
                Description = medic.Description,
                Hidden = medic.Hidden
            }; ;
        }
    }
}
