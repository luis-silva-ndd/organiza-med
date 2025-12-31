using AutoMapper;
using Organiza_Med.ModuloAtividade;
using Organiza_Med.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mapping.Actions;
public class ConfigurarMedicoMappingAction
	: IMappingAction<FormsAtividadeViewModel, Atividade>
{
	private readonly IRepositorioMedico _repositorioMedico;

	public ConfigurarMedicoMappingAction(IRepositorioMedico repositorioMedico)
	{
		_repositorioMedico = repositorioMedico;
	}

	public async void Process(FormsAtividadeViewModel source, Atividade destination, ResolutionContext context)
	{
		var idMedico = source.MedicoId;

		// Aguarda a execução do método assíncrono para obter os médicos
		destination.Medicos = await _repositorioMedico.SelecionarTodosAsync();
	}
}
