using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using OrganizaMed.Infra.Compartilhado;

namespace OrganizaMed.Testes.Integracao.Compartilhado
{
    public class TestsIntegracaoBase
    {
        protected OrganizaMedDbContext DbContext { get; }

        protected TestsIntegracaoBase()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<TestsIntegracaoBase>()
                .Build();
            var options = new DbContextOptionsBuilder<OrganizaMedDbContext>()
                .UseSqlServer(config["SQLSERVER_CONNECTION_STRING"])
                .Options;
            DbContext = new OrganizaMedDbContext(options);
        }
    }
}
