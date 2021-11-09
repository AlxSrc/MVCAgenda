using MVCAgenda.Core.Domain;

namespace MVCAgenda.Core
{
    public class BaseSoftDeleteEntity : BaseEntity 
    {
        public bool Hidden { get; set; }
    }
}