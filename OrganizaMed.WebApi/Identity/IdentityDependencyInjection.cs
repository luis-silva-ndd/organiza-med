using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Organiza_Med.ModuloAutenticacao;
using OrganizaMed.Aplicacao.ModuloAutenticacao;
using OrganizaMed.Infra.Compartilhado;
using OrganizaMed.WebApi.Identity;

namespace OrganizaMed.WebApi.Identity;

public static class IdentityDependencyInjection
{
	public static void ConfigureIdentity(this IServiceCollection services)
	{
		services.AddScoped<ServicoAutenticacao>();
		services.AddScoped<JsonWebTokenProvider>();
		services.AddScoped<ITenantProvider, ApiTenantProvider>();

		services.AddIdentity<Usuario, Cargo>(options =>
			{
				options.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<OrganizaMedDbContext>()
			.AddDefaultTokenProviders();
	}

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration config)
	{
		var chaveAssinaturaJwt = config["JWT_GENERATION_KEY"];

		if (chaveAssinaturaJwt == null)
			throw new ArgumentException("Não foi possível obter a chave de assinatura de tokens.");

		var chaveEmBytes = Encoding.ASCII.GetBytes(chaveAssinaturaJwt);

		var audienciaValida = config["JWT_AUDIENCE_DOMAIN"];

		if (audienciaValida == null)
			throw new ArgumentException("Não foi possível obter o domínio da audiência dos tokens.");

		services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(x =>
		{
			x.RequireHttpsMetadata = true;
			x.SaveToken = true;

			x.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(chaveEmBytes),
				ValidAudience = audienciaValida,
				ValidIssuer = "OrganizaMed",
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateLifetime = true
			};
		});
	}
}