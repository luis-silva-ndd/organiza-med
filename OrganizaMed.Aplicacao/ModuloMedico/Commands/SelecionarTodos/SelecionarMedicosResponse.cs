namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarTodos;

public record SelecionarMedicosResponse
{
    public int QuantidadeRegistros { get; init; }
    public IEnumerable<SelecionarMedicosDto> Registros { get; init; }

}

public record SelecionarMedicosDto(Guid Id, string Nome, string Crm);