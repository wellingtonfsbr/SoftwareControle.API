using FluentValidation;
using SoftwareControle.Models;

namespace SoftwareControle.Service.Validacao;

public class RelatorioValidacao : AbstractValidator<RelatorioModel>
{
    public RelatorioValidacao()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Campo de descrição do relatorio não pode estar vazio")
            .MinimumLength(2).WithMessage("Campo de descrição do relatorio tem que ter no minimo 2 caracteres")
            .MaximumLength(3000).WithMessage("Campo descrição do relatorio pode ter no maximo 3000 caracteres");
    }
}
