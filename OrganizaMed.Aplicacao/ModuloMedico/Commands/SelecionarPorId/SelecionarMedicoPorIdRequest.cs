using FluentResults;
using MediatR;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.SelecionarPorId;

public record SelecionarMedicoPorIdRequest(Guid id) : IRequest<Result<SelecionarMedicoPorIdResponse>>;