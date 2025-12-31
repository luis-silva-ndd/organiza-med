using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Editar;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Excluir;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Inserir;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarPorId;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarTodos;
using OrganizaMed.WebApi.Extensions;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers;

[Route("api/medicos")]
[ApiController]
public class MedicoController(IMediator mediator) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(typeof(InserirMedicoResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarMedicosRequest());

        return resultado.ToHttpResponse();
    }

	[HttpGet("{id}")]
	[ProducesResponseType(typeof(SelecionarMedicoPorIdResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarMedicoPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
	}

	[HttpPost]
	[ProducesResponseType(typeof(InserirMedicoResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> Inserir(InserirMedicoRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarMedicoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarMedicoPartialRequest resquest)
    {
        var editarRequest = new EditarMedicoRequest(
            id,
            resquest.Nome,
            resquest.Crm
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirMedicoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirMedicoRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Put(Guid id, EditarMedicoViewModel medicoVm)
    //{
    //	var selecaoMedicoOriginal = await servicoMedico.SelecionarPorIdAsync(id);

    //	var medicoEditada = mapeador.Map(medicoVm, selecaoMedicoOriginal.Value);

    //	var edicaoResult = await servicoMedico.EditarAsync(medicoEditada);

    //	if (edicaoResult.IsFailed)
    //		return BadRequest(edicaoResult.Errors);

    //	return Ok(edicaoResult.Value);
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //	var medicoResult = await servicoMedico.ExcluirAsync(id);

    //	if (medicoResult.IsFailed)
    //		return NotFound(medicoResult.Errors);

    //	return Ok();
    //}
}