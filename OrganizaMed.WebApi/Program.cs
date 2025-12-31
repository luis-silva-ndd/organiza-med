using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloAtividade;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.ModuloAtividade;
using OrganizaMed.Aplicacao.ModuloAutenticacao;
using OrganizaMed.Aplicacao.ModuloMedico;
using OrganizaMed.Infra.Compartilhado;
using OrganizaMed.Infra.ModuloAtividade;
using OrganizaMed.Infra.ModuloMedico;
using OrganizaMed.WebApi.Config;
using OrganizaMed.WebApi.Config.Mapping;
using OrganizaMed.WebApi.Config.Mapping.Actions;
using OrganizaMed.WebApi.Identity;
using Serilog;

namespace OrganizaMed.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        const string corsPolicyName = "PoliticaCorsOrganizaMed";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureSerilog(builder.Logging, builder.Configuration);

        builder.Services.ConfigureDbContext(builder.Configuration, builder.Environment);

        builder.Services.ConfigureFluentValidation();

        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureMediatR();

        builder.Services.ConfigureControllersWithFilters();

        builder.Services.ConfigureOpenApiAuthHeaders();

        builder.Services.ConfigureCorsPolicy(corsPolicyName);

        var app = builder.Build();

        app.UseGlobalExceptionHandler();

        var migracoesRealizadas = app.AutoMigrateDatabase();

        Log.Debug(migracoesRealizadas ? "Migrações de banco de dados realizadas." : "Nenhuma migração no banco de dados aplicada");

        app.UseSwagger();

        //var connectionString = builder.Configuration.GetConnectionString("SqlServer");

        //builder.Services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
        //{
        //    optionsBuilder.UseSqlServer(connectionString, dbOptions => dbOptions.EnableRetryOnFailure());
        //});

        //builder.Services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
        //builder.Services.AddScoped<ServicoMedico>();

        //builder.Services.AddScoped<IRepositorioAtividade, RepositorioAtividadeOrm>();
        //builder.Services.AddScoped<ServicoAtividade>();

        //builder.Services.AddScoped<ConfigurarMedicoMappingAction>();

        //builder.Services.AddAutoMapper(config =>
        //{
        //    config.AddProfile<MedicoProfile>();
        //    config.AddProfile<AtividadeProfile>();
        //});

        //// Configuração de comunicação entre servidores em domínios diferentes (CORS)
        //// Docs: https://learn.microsoft.com/pt-br/aspnet/core/security/cors?view=aspnetcore-8.0
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy(name: politicaCorsPersonalizada, policy =>
        //    {
        //        policy
        //            .AllowAnyOrigin()
        //            .AllowAnyHeader()
        //            .AllowAnyMethod();
        //    });
        //});

        //builder.Services.AddControllers();

        //builder.Services.AddEndpointsApiExplorer();

        //builder.Services.AddSwaggerGen();

        //// Middlewares de execução da API
        //var app = builder.Build();

        //app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseCors(corsPolicyName);

        app.UseAuthentication();

        app.UseAuthorization();
        //// Migrações de banco de dados
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>();

            if (dbContext is OrganizaMedDbContext organizaMedDbContext)
            {
                MigradorBancoDados.AtualizarBancoDados(organizaMedDbContext);
            }
        }

        //app.UseHttpsRedirection();

        //// Habilitando CORS através de middleware
        //app.UseCors(politicaCorsPersonalizada);

        app.MapControllers();

        app.Run();
    }
}
