using Organiza_Med.ModuloAutenticacao;

namespace Organiza_Med.Compartilhado;

public abstract class EntidadeBase
{
    public Guid Id { get; set; }
    public bool Ativo { get; set; } = true;

    public EntidadeBase()
    {
        Id = Guid.NewGuid();
    }
}
