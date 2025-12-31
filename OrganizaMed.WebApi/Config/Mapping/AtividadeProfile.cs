using AutoMapper;
using Organiza_Med.ModuloAtividade;
using OrganizaMed.WebApi.Config.Mapping.Actions;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mapping;
	public class AtividadeProfile : Profile
	{
		public AtividadeProfile()
		{
			CreateMap<Atividade, ListarAtividadeViewModel>();
			CreateMap<Atividade, VisualizarAtividadeViewModel>();

			CreateMap<FormsAtividadeViewModel, Atividade>()
				.AfterMap<ConfigurarMedicoMappingAction>();
		}
	}
