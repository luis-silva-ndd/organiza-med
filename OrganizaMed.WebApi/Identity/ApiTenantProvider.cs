using System.Security.Claims;
using Organiza_Med.ModuloAutenticacao;

namespace OrganizaMed.WebApi.Identity;

public class ApiTenantProvider : ITenantProvider
{
	private readonly IHttpContextAccessor contextAccessor;

	public ApiTenantProvider(IHttpContextAccessor contextAccessor)
	{
		this.contextAccessor = contextAccessor;
	}

	public Guid? UsuarioId
	{
		get
		{
			var claimId = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

			if (claimId == null)
				return null;

			return Guid.Parse(claimId.Value);
		}
	}
}