using MVCAgenda.ApiHost.DTOs.Medics;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Medics
{
    public interface IMedicsFactory
    {
        MedicDto PrepereDTO(Medic medic);
    }
}
