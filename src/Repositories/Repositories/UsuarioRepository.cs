using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AplicacaoContext _context;

    public UsuarioRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetByLogin(string login)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
    }
}