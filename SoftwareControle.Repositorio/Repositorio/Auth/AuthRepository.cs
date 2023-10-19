using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;
using SoftwareControle.Repositorio.Context;

namespace HFAcademia.Repositório;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;

    public AuthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioModel?> Login(UsuarioModel usuario, CancellationToken cancellationToken)
    {
        UsuarioModel? requestedUser = await _context.Usuarios
            .SingleOrDefaultAsync(u => u.Usuario == usuario.Usuario && u.Senha == usuario.Senha,
                cancellationToken);

        if (requestedUser is null)
            return null;

        return requestedUser;
    }
}
