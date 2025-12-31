using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloMedico;

namespace Organiza_Med.ModuloAtividade;

public class Atividade : EntidadeBase
{
    public TipoAtividadeEnum Tipo { get; set; }
    public DateTime HorarioInicio { get; set; }
    public DateTime HorarioTermino { get; set; }
    public List<Medico> Medicos { get; set; }

    protected Atividade(){}
    public Atividade(TipoAtividadeEnum tipo,DateTime horarioInicio, DateTime horarioTermino, List<Medico> medicos)
    {
        Tipo = tipo;
        HorarioInicio = horarioInicio;
        HorarioTermino = horarioTermino;
        Medicos = medicos;
    }

    public TimeSpan ObterPeriodoDescanso()
    {
        if (TempoDescanso.Valores.TryGetValue(Tipo, out var periodoDescanso))
        {
            return periodoDescanso;
        }

        throw new InvalidOperationException("Tipo de atividade não encontrado.");
    }
}