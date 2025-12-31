using FluentResults;
using Organiza_Med.ModuloAtividade;

namespace OrganizaMed.Aplicacao.ModuloAtividade;
public class ServicoAtividade
{
    private readonly IRepositorioAtividade _repositorioAtividade;

    public ServicoAtividade(IRepositorioAtividade repositorioAtividade)
    {
        _repositorioAtividade = repositorioAtividade;
    }

    public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
    {
        if (atividade.Tipo == TipoAtividadeEnum.Consulta && atividade.Medicos.Count != 1)
            return Result.Fail("Uma consulta deve ser realizada por apenas um médico.");

        if (atividade.Tipo == TipoAtividadeEnum.Cirurgia && atividade.Medicos.Count < 1)
            return Result.Fail("Uma cirurgia deve ser realizada por pelo menos um médico.");

        if (atividade.HorarioInicio >= atividade.HorarioTermino)
            return Result.Fail("O horário de término deve ser maior que o horário de início.");
        TimeSpan periodoDescanso = atividade.ObterPeriodoDescanso();


        var atividadesExistentes = new List<Atividade>();

        foreach (var medicoId in atividade.Medicos.Select(m => m.Id))
        {
            var atividadesMedico = await _repositorioAtividade.SelecionarPorMedicosAsync(atividade.Medicos.Select(m => m.Id));

            atividadesExistentes.AddRange(atividadesMedico);
        }

        foreach (var atv in atividadesExistentes)
        {
            if (atividade.HorarioInicio < atv.HorarioTermino &&
                atividade.HorarioTermino > atv.HorarioInicio)
            {
                return Result.Fail(
                    $"Conflito de horário com a atividade {atv.Id} para o médico {atv.Medicos.FirstOrDefault(m => atividade.Medicos.Any(am => am.Id == m.Id))?.Crm}.");
            }

            foreach (var medico in atividade.Medicos)
            {
                if (atv.Medicos.Any(m => m.Id == medico.Id))
                {
                    var descansoNecessario = atv.ObterPeriodoDescanso();
                    if (atividade.HorarioInicio < atv.HorarioTermino.Add(descansoNecessario))
                    {
                        return Result.Fail(
                            $"O médico {medico.Crm} precisa de mais tempo de descanso após a atividade {atv.Id}.");
                    }
                }
            }
        }

        var validador = new ValidadorAtividade();

        var resultadoValidacao = await validador.ValidateAsync(atividade);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(erros);
        }

        await _repositorioAtividade.InserirAsync(atividade);

        return Result.Ok(atividade);
    }

    public async Task<Result<Atividade>> EditarAsync(Atividade atividade)
    {
        var validador = new ValidadorAtividade();

        var resultadoValidacao = await validador.ValidateAsync(atividade);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(erros);
        }

        _repositorioAtividade.Editar(atividade);

        return Result.Ok(atividade);
    }

    public async Task<Result> ExcluirAsync(Guid id)
    {
        var atividade = await _repositorioAtividade.SelecionarPorIdAsync(id);

        if (atividade == null)
            return Result.Fail($"Atividade {id} não encontrada");

        _repositorioAtividade.Excluir(atividade);

        return Result.Ok();
    }

    public async Task<Result<List<Atividade>>> SelecionarTodosAsync()
    {
        var atividades = await _repositorioAtividade.SelecionarTodosAsync();

        return Result.Ok(atividades);
    }

    public async Task<Result<Atividade>> SelecionarPorIdAsync(Guid id)
    {
        var atividade = await _repositorioAtividade.SelecionarPorIdAsync(id);

        return Result.Ok(atividade);
    }
}