using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareControle.Models;

namespace SoftwareControle.Map;

public class FerramentaMap : BaseMap<FerramentaModel>
{
	public FerramentaMap() : base("Ferramentas")
	{
	}

	public override void Configure(EntityTypeBuilder<FerramentaModel> builder)
	{
		base.Configure(builder);

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasMaxLength(64);
		builder.Property(x => x.Descricao).IsRequired().HasColumnName("Descricao").HasMaxLength(500);
		builder.Property(x => x.Imagem).HasColumnName("Imagem");
        builder.Property(x => x.DataCriacao).IsRequired().HasColumnName("DataCriacao");
        builder.Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");
    }
}
