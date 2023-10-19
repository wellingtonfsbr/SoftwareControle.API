using FluentValidation;
using SoftwareControle.Models;

namespace SoftwareControle.Service.Validacao;

public class FerramentaValidacao : AbstractValidator<FerramentaModel>
{
    public FerramentaValidacao()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo de nome da ferramenta não pode estar vazio")
            .MinimumLength(2).WithMessage("Campo de nome da ferramenta tem que ter no minimo 2 caracteres")
            .MaximumLength(64).WithMessage("Campo nome da ferramenta pode ter no maximo 64 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Campo de descrição da ferramente não pode estar vazio")
            .MinimumLength(2).WithMessage("Campo de descrição da ferramente tem que ter no minimo 2 caracteres")
            .MaximumLength(64).WithMessage("Campo de descrição da ferramente pode ter no maximo 64 caracteres");
    }
}
