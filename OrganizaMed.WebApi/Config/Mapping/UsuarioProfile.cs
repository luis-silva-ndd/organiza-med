using AutoMapper;
using Organiza_Med.ModuloAutenticacao;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mapping
{
	public class UsuarioProfile : Profile
	{
		public UsuarioProfile()
		{
			CreateMap<RegistrarUsuarioViewModel, Usuario>();
		}
	}
}
