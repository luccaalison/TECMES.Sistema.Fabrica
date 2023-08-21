using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AplicacaoContext _context;

    public ProdutoRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAll()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto> GetById(int id)
    {
        return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
    }
}