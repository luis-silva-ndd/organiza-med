using FluentResults;
using MediatR;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarTodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizaMed.Aplicacao.Compartilhado;
using FluentValidation;
using Organiza_Med.Compartilhado;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Inserir;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarPorId;


public class SelecionarMedicoPorIdRequestHandler(IRepositorioMedico repositorioMedico)
    : IRequestHandler<SelecionarMedicoPorIdRequest, Result<SelecionarMedicoPorIdResponse>>
{
    public async Task<Result<SelecionarMedicoPorIdResponse>> Handle(SelecionarMedicoPorIdRequest request, CancellationToken cancellationToken)
    {
        var medicoSelecionado = await repositorioMedico.SelecionarPorIdAsync(request.id);

        if (medicoSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.id));

        var resposta = new SelecionarMedicoPorIdResponse(medicoSelecionado.Id, medicoSelecionado.Nome, medicoSelecionado.Crm);

        return Result.Ok(resposta);
    }
}