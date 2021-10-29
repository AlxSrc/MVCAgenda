using Microsoft.AspNetCore.Http;
using MVCAgenda.Core;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCAgenda.Framework
{
	public class WebWorkContext : IWorkContext
	{
		#region Fields
		
		private readonly IHttpContextAccessor _httpContextAccessor;

		#endregion

		#region Ctor

		public WebWorkContext(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

        #endregion

        #region Methods
        public Task<ClaimsPrincipal> GetCurrentUserAsync()
		{
			return Task.FromResult(_httpContextAccessor?.HttpContext?.User as ClaimsPrincipal);
		}
		#endregion
	}
}
