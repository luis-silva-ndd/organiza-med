using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Infra.Compartilhado;
using System.Collections.Generic;
using Organiza_Med.ModuloAtividade;

namespace OrganizaMed.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        var novoMedico1 = new Medico("123446-SC", "12345-SP");
        var novoMedico2 = new Medico("123445-SC", "12345-SP");
        List<Medico> medicos = new List<Medico>();
        medicos.Add(novoMedico1);
        medicos.Add(novoMedico2);
        var novaAtividade = new Atividade(TipoAtividadeEnum.Cirurgia, DateTime.MinValue, DateTime.MaxValue, medicos);

        var optionsBuilder = new DbContextOptionsBuilder<OrganizaMedDbContext>();

        var configuracao = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuracao.GetConnectionString("SqlServer");

        optionsBuilder.UseSqlServer(connectionString);
        
        var dbContext = new OrganizaMedDbContext(optionsBuilder.Options);

        dbContext.Add(novoMedico1);
        dbContext.Add(novoMedico2);
        dbContext.Add(novaAtividade);

        dbContext.SaveChanges();

        Console.ReadLine();
    }
}