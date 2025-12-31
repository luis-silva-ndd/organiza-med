using FluentValidation;

namespace Organiza_Med.ModuloMedico;

public class ValidadorMedico : AbstractValidator<Medico>
{
    public ValidadorMedico()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .DependentRules(() =>
            {
                RuleFor(m => m.Nome).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLenght} caracteres.");
            });
        RuleFor(m => m.Crm)
        .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
        .Matches(@"^\d{5}-[A-Z]{2}$").WithMessage("O campo {PropertyName} deve estar no formato 00000-UF.");

    }
}