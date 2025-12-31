using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Infra.ModuloMedico;
using OrganizaMed.Testes.Integracao.Compartilhado;

namespace OrganizaMed.Testes.Integracao.ModuloMedico;

[TestClass]
[TestCategory("Testes de Integração")]
public class RepositorioMedicoOrmTests : TestsIntegracaoBase
{
    private readonly IRepositorioMedico _repositorioMedicoOrm;
    public RepositorioMedicoOrmTests()
    {
        _repositorioMedicoOrm = new RepositorioMedicoOrm(DbContext);
    }

    [TestInitialize]
    public void Inicializar()
    {
        Debug.WriteLine("Criando banco de dados");
        DbContext.Database.EnsureDeleted();

        DbContext.Database.Migrate();
        Debug.WriteLine("Banco de dados criado com sucesso");
    }

    [TestMethod]
    public async Task Deve_Inserir_Medico_Corretamente()
    {
        var entidadeOriginal = new Medico("12345-SC", "Vanessa");

        var id = await _repositorioMedicoOrm.InserirAsync(entidadeOriginal);

        await DbContext.SaveChangesAsync();

        var entidadeSelecionada = await _repositorioMedicoOrm.SelecionarPorIdAsync(id);

        Assert.IsNotNull(entidadeSelecionada);
        Assert.AreEqual(entidadeOriginal, entidadeSelecionada);
    }
    [TestMethod]
    public async Task Deve_Editar_Medico_Corretamente()
    {
        var entidadeOriginal = new Medico("12345-SC", "Vanessa");

        var id = await _repositorioMedicoOrm.InserirAsync(entidadeOriginal);
        await DbContext.SaveChangesAsync();

        var entidadeParaEditar = await _repositorioMedicoOrm.SelecionarPorIdAsync(id);
        entidadeParaEditar.Nome = "Vanessa Editada";

        await _repositorioMedicoOrm.EditarAsync(entidadeParaEditar);
        await DbContext.SaveChangesAsync();

        var entidadeEditada = await _repositorioMedicoOrm.SelecionarPorIdAsync(id);

        Assert.IsNotNull(entidadeEditada);
        Assert.AreEqual("Vanessa Editada", entidadeEditada.Nome);
    }

    [TestMethod]
    public async Task Deve_Excluir_Medico_Corretamente()
    {
        var entidadeOriginal = new Medico("12345-SC", "Vanessa");

        var id = await _repositorioMedicoOrm.InserirAsync(entidadeOriginal);
        await DbContext.SaveChangesAsync();

        var entidadeParaExcluir = await _repositorioMedicoOrm.SelecionarPorIdAsync(id);
        await _repositorioMedicoOrm.ExcluirAsync(entidadeParaExcluir);
        await DbContext.SaveChangesAsync();

        var entidadeExcluida = await _repositorioMedicoOrm.SelecionarPorIdAsync(id);

        Assert.IsNull(entidadeExcluida);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Medico_Corretamente()
    {
        var entidadeOriginal = new Medico("12345-SC", "Vanessa");

        List<Medico> medicos = new List<Medico>
        {
            new Medico("12345-SC", "Luis"),
            new Medico("12345-SC", "Vitoria")
        };

        foreach (var medico in medicos)
            await _repositorioMedicoOrm.InserirAsync(medico);

        await DbContext.SaveChangesAsync();

        var entidades = await _repositorioMedicoOrm.SelecionarTodosAsync();

        CollectionAssert.AreEqual(medicos, entidades.ToList());
    }
}