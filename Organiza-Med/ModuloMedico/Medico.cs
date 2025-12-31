using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloAtividade;
using System.Text.Json.Serialization;

namespace Organiza_Med.ModuloMedico;

public class Medico : EntidadeBase
{
    public string Nome { get; set; }
    public string Crm { get; set; }
    [JsonIgnore] public List<Atividade> Atividades { get; set; }

    protected Medico()
    {
        Atividades = [];
    }
    public Medico(string nome, string crm) : this()
    {
        Nome = nome;
        Crm = crm;
    }

    public void AdicionarAtividade(Atividade atividade)
    {
        ArgumentNullException.ThrowIfNull(atividade);

        if (!Atividades.Contains(atividade))
            Atividades.Add(atividade);
    }
    public void RemoverAtividade(Atividade atividade)
    {
        ArgumentNullException.ThrowIfNull(atividade);
        Atividades.Remove(atividade);
    }
}