using MVCAgenda.ApiHost.DTOs.Medics;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Medics
{
    public class MedicsFactory : IMedicsFactory
    {
        public MedicDto PrepereDTO(Medic medic)
        {
            return new MedicDto()
            {
                Id = medic.Id,
                Name = medic.Name,
                Mail = medic.Mail,
                ImagePath = medic.ImagePath,
                Description = medic.Description,
                Designation = medic.Designation,
                Hidden = medic.Hidden
            };
        }
    }
}
