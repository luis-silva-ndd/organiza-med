using Microsoft.EntityFrameworkCore;
using Organiza_Med.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizaMed.Infra.Compartilhado;
public class RepositorioBase<TEntidade> where TEntidade : EntidadeBase
{
    protected readonly IContextoPersistencia ctx;
    protected readonly DbSet<TEntidade> registros;


    public RepositorioBase(IContextoPersistencia ctx)
    {
        this.ctx = ctx;
        registros = ((DbContext)this.ctx).Set<TEntidade>();
    }

    public async Task<Guid> InserirAsync(TEntidade registro)
    {
        await registros.AddAsync(registro);
          
        return registro.Id;
    }

    public async Task<bool> EditarAsync(TEntidade registro)
    {
        var rastreador = registros.Update(registro);

        return await Task.Run(() => rastreador.State == EntityState.Modified);
    }

    public async Task<bool> ExcluirAsync(TEntidade registro)
    {
        var rastreador = registros.Remove(registro);

        return await Task.Run(() => rastreador.State == EntityState.Deleted);
    }

    public async virtual Task<TEntidade> SelecionarPorIdAsync(Guid id)
    {
        return await registros.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async virtual Task<List<TEntidade>> SelecionarTodosAsync()
    {
        return await registros.ToListAsync();
    }
}