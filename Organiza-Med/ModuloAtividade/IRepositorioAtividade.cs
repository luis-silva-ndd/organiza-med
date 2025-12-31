using Organiza_Med.Compartilhado;

namespace Organiza_Med.ModuloAtividade;

public interface IRepositorioAtividade : IRepositorioBase<Atividade>
{
    Task<List<Atividade>> Filtrar(Func<Atividade, bool> predicate);
    Task<List<Atividade>> SelecionarPorMedicosAsync(IEnumerable<Guid> medicosIds);
}