using Organiza_Med.ModuloAtividade;
namespace OrganizaMed.WebApi.ViewModels;

//public TipoAtividadeEnum Tipo { get; set; }
//public DateTime HorarioInicio { get; set; }
//public DateTime HorarioTermino { get; set; }
//public List<Medico> Medicos { get; set; }
public class ListarAtividadeViewModel
{
	public required Guid Id { get; set; }
	public required TipoAtividadeEnum Tipo { get; set; }
	public DateTime HorarioInicio { get; set; }
	public DateTime HorarioFim { get; set; }
	public required List<ListarMedicoViewModel> Medicos { get; set; }
}

public class VisualizarAtividadeViewModel
{
	public required Guid Id { get; set; }
	public required TipoAtividadeEnum Tipo { get; set; }
	public DateTime HorarioInicio { get; set; }
	public DateTime HorarioFim { get; set; }
	public required List<ListarMedicoViewModel> Medicos { get; set; }
}

public class FormsAtividadeViewModel
{
	public required TipoAtividadeEnum Tipo { get; set; }
	public DateTime HorarioInicio { get; set; }
	public DateTime HorarioFim { get; set; }
	public required Guid MedicoId { get; set; }
}

public class InserirAtividadeViewModel : FormsAtividadeViewModel
{
}

public class EditarAtividadeViewModel : FormsAtividadeViewModel
{
}