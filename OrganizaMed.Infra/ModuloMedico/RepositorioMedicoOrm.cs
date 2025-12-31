using Microsoft.Win32;
using Organiza_Med.Compartilhado;
using OrganizaMed.Infra.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organiza_Med.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Organiza_Med.ModuloAtividade;

namespace OrganizaMed.Infra.ModuloMedico;

public class RepositorioMedicoOrm(IContextoPersistencia context) : RepositorioBase<Medico>(context), IRepositorioMedico
{
    public Task<Medico?> SelecionarPorCRMAsync(string crm)
    {
        throw new NotImplementedException();
    }

    public override async Task<List<Medico>> SelecionarTodosAsync()
    {
        return await registros.Where(x => x.Ativo == true).Include(x => x.Atividades).ToListAsync();
    }

    public override async Task<Medico> SelecionarPorIdAsync(Guid id)
    {
        return await registros.Where(x => x.Ativo == true).Include(x => x.Atividades).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Medico>> SelecionarMuitosPorId(IEnumerable<Guid> medicosRequisitados)
    {
        return await registros.Where(m => medicosRequisitados.Contains(m.Id)).ToListAsync();
    }
}
