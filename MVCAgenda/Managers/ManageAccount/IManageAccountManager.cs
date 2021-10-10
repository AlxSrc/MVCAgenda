using MVCAgenda.Models.Accounts.ManageAccount;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.ManageAccount
{
    public interface IManageAccountManager
    {
        Task<ProfileViewModel> GetUserProfileAsync(string accountId);
    }
}