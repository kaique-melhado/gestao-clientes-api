using FluentValidation;
using GestaoClientes.Application.DTOs;

namespace GestaoClientes.Application.Validators;

public class TelefoneDTOValidator : AbstractValidator<TelefoneDTO>
{
    public TelefoneDTOValidator()
    {
        RuleFor(x=> x.DDD)
            .NotNull()
            .NotEmpty()
            .WithMessage("O DDD é obrigatório.");

        RuleFor(x=> x.DDD)
            .Length(2)
            .WithMessage("O DDD deve ter conter 2 caracteres.");

        RuleFor(x=> x.Numero)
                .NotEmpty()
                .WithMessage("O número de telefone é obrigatório.");

        RuleFor(x=> x.Numero)
            .Length(8, 9)
            .WithMessage("O número de telefone deve conter entre 8 e 9 caracteres.");

        RuleFor(x => x.Tipo)
            .NotNull()
            .NotEmpty()
            .WithMessage("O tipo de telefone é obrigatório.");

        RuleFor(x=> x.Tipo)
            .IsInEnum()
            .WithMessage("O tipo de telefone é inválido.");
    }
}
