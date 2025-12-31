using FluentResults;
using Organiza_Med.ModuloMedico;

namespace OrganizaMed.Aplicacao.ModuloMedico;
public class ServicoMedico
{
    private readonly IRepositorioMedico _repositorioMedico;

    public ServicoMedico(IRepositorioMedico repositorioMedico)
    {
        _repositorioMedico = repositorioMedico;
    }

    public async Task<Result<Medico>> InserirAsync(Medico medico)
    {
        var validador = new ValidadorMedico();

        var existente = await _repositorioMedico.SelecionarPorCRMAsync(medico.Crm);
        if (existente != null)
            return Result.Fail($"Já existe um médico cadastrado com o CRM {medico.Crm}.");

        var resultadoValidacao = await validador.ValidateAsync(medico);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(erros);
        }

        await _repositorioMedico.InserirAsync(medico);

        return Result.Ok(medico);
    }

    public async Task<Result<Medico>> EditarAsync(Medico medico)
    {
        var validador = new ValidadorMedico();

        var resultadoValidacao = await validador.ValidateAsync(medico);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(erros);
        }

        _repositorioMedico.EditarAsync(medico);

        return Result.Ok(medico);
    }

    public async Task<Result> ExcluirAsync(Guid id)
    {
        var medico = await _repositorioMedico.SelecionarPorIdAsync(id);

        if (medico == null)
            return Result.Fail($"Medico {id} não encontrado");
        
        Desativar(medico);

        return Result.Ok();
    }

    private void Desativar(Medico medico)
    {
        medico.Ativo = false;
        _repositorioMedico.EditarAsync(medico);
    }

    public async Task<Result<List<Medico>>> SelecionarTodosAsync()
    {
        var medicos = await _repositorioMedico.SelecionarTodosAsync();

        return Result.Ok(medicos);
    }

    public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
    {
        var medico = await _repositorioMedico.SelecionarPorIdAsync(id);

        return Result.Ok(medico);
    }
}