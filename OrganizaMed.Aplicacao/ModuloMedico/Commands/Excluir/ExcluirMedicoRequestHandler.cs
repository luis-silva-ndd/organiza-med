using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.Compartilhado;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.Excluir;

public class ExcluirMedicoRequestHandler(
    IRepositorioMedico repositorioMedico,
    IContextoPersistencia ctx) : IRequestHandler<ExcluirMedicoRequest, Result<ExcluirMedicoResponse>>
{
    public async Task<Result<ExcluirMedicoResponse>> Handle(ExcluirMedicoRequest request, CancellationToken cancellationToken)
    {
        var medicoSelecionado = await repositorioMedico.SelecionarPorIdAsync(request.Id);

        if (medicoSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioMedico.ExcluirAsync(medicoSelecionado);

            await ctx.GravarAsync();
        }
        catch (Exception ex)
        {
            await ctx.RollBackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirMedicoResponse());
    }
}