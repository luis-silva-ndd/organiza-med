using FluentValidation;
using Organiza_Med.ModuloMedico;

namespace Organiza_Med.ModuloAtividade;

public class ValidadorAtividade : AbstractValidator<Atividade>
{
    public ValidadorAtividade()
    {
        RuleFor(x => x.Tipo)
            .NotEmpty()
            .WithMessage("O tipo é obrigatório");
    }
}