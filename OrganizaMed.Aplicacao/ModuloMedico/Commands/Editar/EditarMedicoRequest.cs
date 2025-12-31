using FluentResults;
using MediatR;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.Editar;

public record EditarMedicoPartialRequest(string Nome, string Crm);

public record EditarMedicoRequest(Guid id, string Nome, string Crm) : IRequest<Result<EditarMedicoResponse>>;