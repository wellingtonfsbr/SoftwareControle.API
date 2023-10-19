using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareControle.Map;
using SoftwareControle.Models;

namespace SoftwareControle.Repositorio.Map;

public class RelatorioMap : BaseMap<RelatorioModel>
{
    public RelatorioMap() : base("Relatorios")
    {

    }
    public override void Configure(EntityTypeBuilder<RelatorioModel> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao).IsRequired().HasColumnName("Descricao")
            .HasMaxLength(3000);
        builder.Property(x => x.NomeFerramenta).HasColumnName("NomeFerramenta")
            .HasMaxLength(64);
        builder.Property(x => x.NomeUsuario).HasColumnName("NomeUsuario")
            .HasMaxLength(64);
        builder.Property(x => x.DataCriacao).HasColumnName("DataCriacao");
    } 
}