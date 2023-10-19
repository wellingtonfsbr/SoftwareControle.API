using FluentValidation;
using SoftwareControle.Models;

namespace SoftwareControle.Service.Validacao;

public class OrdemValidacao : AbstractValidator<OrdemModel>
{
    public OrdemValidacao()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Campo de descrição da ordem não pode estar vazio")
            .MinimumLength(2).WithMessage("Campo de descrição da ordem tem que ter no minimo 2 caracteres")
            .MaximumLength(3000).WithMessage("Campo de descrição da ordem pode ter no maximo 3000 caracteres");
    }
}
