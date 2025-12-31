using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace OrganizaMed.Aplicacao.ModuloMedico.Commands.Inserir;
public record InserirMedicoRequest(string Nome, string Crm) : IRequest<Result<InserirMedicoResponse>>;