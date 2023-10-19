namespace SoftwareControle.Website.Session
{
	public interface ISessaoUsuario
	{
		Task<Guid> BuscarIdDoUsuarioLogado();
	}
}