using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloAtividade;
using OrganizaMed.Infra.Compartilhado;

namespace OrganizaMed.Infra.ModuloAtividade;
public class RepositorioAtividadeOrm : RepositorioBase<Atividade>, IRepositorioAtividade
{
    public RepositorioAtividadeOrm(IContextoPersistencia dbContext) : base(dbContext)
    {

    }

    public Task<bool> InserirAsync(Atividade registro)
    {
        throw new NotImplementedException();
    }

    public void Editar(Atividade registro)
    {
        throw new NotImplementedException();
    }

    public void Excluir(Atividade registro)
    {
        throw new NotImplementedException();
    }

    public Atividade SelecionarPorId(Guid id)
    {
        return registros.Include(x => x.Medicos).SingleOrDefault(x => x.Id == id);
    }

    public override async Task<Atividade> SelecionarPorIdAsync(Guid id)
    {
        return await registros.Include(x => x.Medicos).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Atividade>> SelecionarPorMedicosAsync(IEnumerable<Guid> medicosIds)
    {
        throw new NotImplementedException();
    }

    public Task<List<Atividade>> Filtrar(Func<Atividade, bool> predicate)
    {
        throw new NotImplementedException();
    }
}