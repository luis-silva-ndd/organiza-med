using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organiza_Med.ModuloAtividade;
using OrganizaMed.Aplicacao.ModuloAtividade;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers;

[Route("api/atividades")]
[ApiController]
public class AtividadeController(ServicoAtividade servicoAtividade, IMapper mapeador) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Get(bool? arquivadas)
	{
		Result<List<Atividade>> atividadesResult;

		atividadesResult = await servicoAtividade.SelecionarTodosAsync();

		var viewModel = mapeador.Map<ListarAtividadeViewModel[]>(atividadesResult.Value);

		return Ok(viewModel);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var atividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

		if (atividadeResult.IsSuccess && atividadeResult.Value is null)
			return NotFound(atividadeResult.Errors);

		var viewModel = mapeador.Map<VisualizarAtividadeViewModel>(atividadeResult.Value);

		return Ok(viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Post(InserirAtividadeViewModel atividadeVm)
	{
		var atividade = mapeador.Map<Atividade>(atividadeVm);

		var atividadeResult = await servicoAtividade.InserirAsync(atividade);

		if (atividadeResult.IsFailed)
			return BadRequest(atividadeResult.Errors);

		return Ok(atividadeVm);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Put(Guid id, EditarAtividadeViewModel atividadeVm)
	{
		var atividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

		if (atividadeResult.IsSuccess && atividadeResult.Value is null)
			return NotFound(atividadeResult.Errors);

		var atividadeEditada = mapeador.Map(atividadeVm, atividadeResult.Value);

		var edicaoResult = await servicoAtividade.EditarAsync(atividadeEditada);

		if (edicaoResult.IsFailed)
			return BadRequest(atividadeResult.Errors);

		return Ok(atividadeVm);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var atividadeResult = await servicoAtividade.ExcluirAsync(id);

		if (atividadeResult.IsFailed)
			return BadRequest(atividadeResult.Errors);

		return Ok();
	}

	//[HttpPut("{id}/alterar-status/")]
	//public async Task<IActionResult> AlterarStatus(Guid id)
	//{
	//	var atividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

	//	if (atividadeResult.IsFailed)
	//		return StatusCode(500);

	//	if (atividadeResult.IsSuccess && atividadeResult.Value is null)
	//		return NotFound(atividadeResult.Errors);

	//	var edicaoResult = servicoAtividade.AlterarStatus(atividadeResult.Value);

	//	if (edicaoResult.IsFailed)
	//		return BadRequest(atividadeResult.Errors);

	//	var atividadeVm = mapeador.Map<VisualizarAtividadeViewModel>(edicaoResult.Value);

	//	return Ok(atividadeVm);
	//}
}