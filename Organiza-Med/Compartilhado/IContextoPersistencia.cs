namespace Organiza_Med.Compartilhado;
public interface IContextoPersistencia
{
    Task<int> GravarAsync();
    Task RollBackAsync();
}