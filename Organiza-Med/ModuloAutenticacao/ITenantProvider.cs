namespace Organiza_Med.ModuloAutenticacao;
public interface ITenantProvider
{
    Guid? UsuarioId { get; }
}
