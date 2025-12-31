namespace OrganizaMed.WebApi.ViewModels;
public class InserirMedicoViewModel
{
	public required string Crm { get; set; }
}

public class EditarMedicoViewModel
{
	public required string Crm { get; set; }
}

public class ListarMedicoViewModel
{
	public required Guid Id { get; set; }
	public required string Crm { get; set; }
}

public class VisualizarMedicoViewModel
{
	public required Guid Id { get; set; }
	public required string Crm { get; set; }
}