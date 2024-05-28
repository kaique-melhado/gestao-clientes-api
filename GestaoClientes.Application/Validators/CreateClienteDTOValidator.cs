using FluentValidation;
using GestaoClientes.Application.DTOs;

namespace GestaoClientes.Application.Validators;

public class CreateClienteDTOValidator : AbstractValidator<CreateClienteDTO>
{
    public CreateClienteDTOValidator()
    {
        RuleFor(x => x.NomeCompleto)
            .NotNull()
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");

        RuleFor(x => x.NomeCompleto)
            .MinimumLength(5)
            .MaximumLength(100)
            .WithMessage("O nome deve conter entre 2 e 100 caracteres.");


        RuleFor(x => x.NomeCompleto)
            .Must(ValidarSobrenome)
            .WithMessage("O nome deve conter pelo menos 1 sobrenome.");


        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.");

        RuleFor(x => x.Email)
            .MaximumLength(100)
            .WithMessage("O e-mail deve conter no máximo 100 caracteres.");

        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("O e-mail é inválido.");

        RuleFor(x => x.Telefones)
            .NotNull()
            .NotEmpty()
            .WithMessage("O telefone é obrigatório.");

        RuleForEach(x => x.Telefones)
            .SetValidator(new TelefoneDTOValidator());
    }

    public bool ValidarSobrenome(string nomeCompleto)
    {
        var separador = nomeCompleto.Trim().Split(' ');
        return separador.Length >= 2;
    }
}
