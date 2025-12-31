using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OrganizaMed.Infra.Compartilhado;

namespace OrganizaMed.Testes.Integracao.Compartilhado;
public class OrganizaMedDbContextFactory : IDesignTimeDbContextFactory<OrganizaMedDbContext>
{
    public OrganizaMedDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<OrganizaMedDbContext>();

        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<OrganizaMedDbContextFactory>()
            .Build();

        builder.UseSqlServer(configuration["SQLSERVER_CONNECTION_STRING"]);

        return new OrganizaMedDbContext(builder.Options);
    }
}