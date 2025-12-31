using Organiza_Med.Compartilhado;

namespace Organiza_Med.ModuloMedico;

public interface IRepositorioMedico
{
    Task<Medico?> SelecionarPorCRMAsync(string crm);
    Task<Guid> InserirAsync(Medico novaEntidade);
    Task<bool> EditarAsync(Medico entidadeAtualizada);
    Task<bool> ExcluirAsync(Medico entidadeParaRemover);
    Task<List<Medico>> SelecionarTodosAsync();
    Task<Medico?> SelecionarPorIdAsync(Guid id);
    Task<List<Medico>> SelecionarMuitosPorId(IEnumerable<Guid> requestMedicos);
}