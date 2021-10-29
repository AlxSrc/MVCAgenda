using MVCAgenda.ApiHost.DTOs.Items;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Managers
{
    public interface IItemsManager
    {
        Task<bool> PostData(ItemsRootObject root);
    }
}
