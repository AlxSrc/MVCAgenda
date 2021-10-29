using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCAgenda.Core
{
	public interface IWorkContext
	{
		Task<ClaimsPrincipal> GetCurrentUserAsync();
	}
}
