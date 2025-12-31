using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrganizaMed.Infra.Compartilhado;
public class OrganizaMedDbContextFactory : IDesignTimeDbContextFactory<OrganizaMedDbContext>
{
    public OrganizaMedDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrganizaMedDbContext>();

        IConfiguration configuracao = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuracao.GetConnectionString("SqlServer");

        optionsBuilder.UseSqlServer(connectionString);

        var dbContext = new OrganizaMedDbContext(optionsBuilder.Options);

        return dbContext;
    }
}