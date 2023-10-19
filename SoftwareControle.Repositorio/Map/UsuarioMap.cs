using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareControle.Models;

namespace SoftwareControle.Map;

public class UsuarioMap : BaseMap<UsuarioModel>
{
	public UsuarioMap() : base("Usuarios")
	{
	}

	public override void Configure(EntityTypeBuilder<UsuarioModel> builder)
	{
		base.Configure(builder);

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Usuario).IsRequired().HasColumnName("Usuario").HasMaxLength(100);
		builder.Property(x => x.Senha).IsRequired().HasColumnName("Senha").HasMaxLength(20);
		builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasMaxLength(100);
		builder.Property(x => x.Cargo).IsRequired().HasColumnName("Cargo").HasMaxLength(100);
		builder.Property(x => x.Imagem).HasColumnName("Imagem");
        builder.Property(x => x.DataCriacao).IsRequired().HasColumnName("DataCriacao").HasMaxLength(100);
		builder.Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");

		builder.HasMany(x => x.Ferramenta).WithOne(x => x.Usuario)
			.HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
