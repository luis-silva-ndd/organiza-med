using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace OrganizaMed.Aplicacao.Compartilhado;

public abstract class ErrorResults
{
    public static Error BadRequestError(List<string> erros)
    {
        return new Error("Requisição formatada incorretamente")
            .CausedBy(erros)
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error NotFoundError(Guid id)
    {
        return new Error("Registro não encontrado")
            .CausedBy($"Não foi possível encontrar o registro com o ID {id}")
            .WithMetadata("ErrorType", "NotFound");
    }
    public static Error InternalServerError(Exception ex)
    {
        return new Error("Erro interno de servidor")
            .CausedBy(new Exception("Erro interno do servidor", ex))
            .WithMetadata("ErrorType", "InternalServer");
    }
}
