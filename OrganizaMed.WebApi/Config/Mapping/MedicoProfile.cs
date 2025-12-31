using AutoMapper;
using Organiza_Med.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mapping;
public class MedicoProfile : Profile
{
    public MedicoProfile()
    {
        CreateMap<Medico, ListarMedicoViewModel>();
        CreateMap<Medico, VisualizarMedicoViewModel>();

        CreateMap<InserirMedicoViewModel, Medico>();
        CreateMap<EditarMedicoViewModel, Medico>();
    }
}
