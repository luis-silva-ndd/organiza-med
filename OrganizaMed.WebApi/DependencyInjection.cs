using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloAtividade;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.ModuloAtividade;
using OrganizaMed.Aplicacao.ModuloMedico;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Inserir;
using OrganizaMed.Infra.Compartilhado;
using OrganizaMed.Infra.ModuloAtividade;
using OrganizaMed.Infra.ModuloMedico;
using OrganizaMed.WebApi.Config.Mapping;
using OrganizaMed.WebApi.Config.Mapping.Actions;
using OrganizaMed.WebApi.Filters;
using Serilog;

namespace OrganizaMed.WebApi;
public static class DepedencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        var connectionString = config["SQLSERVER_CONNECTION_STRING"];

        if (connectionString == null)
            throw new ArgumentNullException("'SQLSERVER_CONNECTIONSTRING' não foi fornecida");

        services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
        {
            if (!environment.IsDevelopment())
                optionsBuilder.EnableSensitiveDataLogging(false);

            optionsBuilder.UseSqlServer(connectionString, dbOptions =>
            {
                dbOptions.EnableRetryOnFailure();
            });
        });
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
    }

    public static void ConfigureControllersWithFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ResponseWrapperFilter>();
        });
    }

    public static void ConfigureOpenApiAuthHeaders(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "organiza-med-api", Version = "v1" });
            options.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "TimeSpan",
                Example = new Microsoft.OpenApi.Any.OpenApiString("00:00:00")
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Informe o token JWT no padrão {Bearer token}",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
    }

    public static void ConfigureCorsPolicy(this IServiceCollection services, string nomePolitica)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: nomePolitica, policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ValidadorMedico>();
    }

    public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging, IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "organiza-med-api",
                licenseKey: "7f94b69437e9750e7aee41c8aa81ef6cFFFFNRAL"
            )
            .CreateLogger();

        logging.ClearProviders();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<InserirMedicoRequest>();
        });
    }
}