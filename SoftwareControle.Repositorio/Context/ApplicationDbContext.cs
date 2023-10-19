using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;

namespace SoftwareControle.Repositorio.Context;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
	public DbSet<UsuarioModel> Usuarios { get; set; }
	public DbSet<FerramentaModel> Ferramentas { get; set; }
	public DbSet<OrdemModel> Ordens { get; set; }
	public DbSet<RelatorioModel> Relatorio { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
