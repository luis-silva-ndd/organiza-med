using FluentResults;
using FluentValidation;
using MediatR;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.Compartilhado;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.Editar;

public class EditarMedicoRequestHandler(
    IRepositorioMedico repositorioMedico,
    IContextoPersistencia ctx,
    IValidator<Medico> validador) : IRequestHandler<EditarMedicoRequest, Result<EditarMedicoResponse>>
{
    public async Task<Result<EditarMedicoResponse>> Handle(EditarMedicoRequest request, CancellationToken cancellationToken)
    {
        var medicoSelecionado = await repositorioMedico.SelecionarPorIdAsync(request.id);

        if (medicoSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.id));

        medicoSelecionado.Nome = request.Nome;
        medicoSelecionado.Crm = request.Crm;

        var resultadoValidacao =
            await validador.ValidateAsync(medicoSelecionado, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var medicos = await repositorioMedico.SelecionarTodosAsync();

        if (NomeDuplicado(medicoSelecionado, medicos))
            return Result.Fail(MedicoErrorResults.NomeDuplicadoError(medicoSelecionado.Nome));

        if (CrmDuplicado(medicoSelecionado, medicos))
            return Result.Fail(MedicoErrorResults.CrmDuplicadoError(medicoSelecionado.Crm));

        try
        {
            await repositorioMedico.EditarAsync(medicoSelecionado);

            await ctx.GravarAsync();
        }
        catch (Exception ex)
        {
            await ctx.RollBackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new EditarMedicoResponse(medicoSelecionado.Id));
    }

    private bool NomeDuplicado(Medico medico, IList<Medico> medicos)
    {
        return medicos.Any(m => m.Nome.Equals(medico.Nome, StringComparison.OrdinalIgnoreCase) && m.Id != medico.Id);
    }

    private bool CrmDuplicado(Medico medico, IList<Medico> medicos)
    {
        return medicos.Any(m => m.Crm.Equals(medico.Crm, StringComparison.OrdinalIgnoreCase) && m.Id != medico.Id);
    }
}