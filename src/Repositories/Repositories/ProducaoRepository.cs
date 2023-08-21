using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class ProducaoRepository : IProducaoRepository
{
    private readonly AplicacaoContext _context;

    public ProducaoRepository(AplicacaoContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Producao>> GetAll()
    {
        return await _context.Producoes.ToListAsync();
    }

    public async Task<Producao> GetById(int id)
    {
        return await _context.Producoes.FindAsync(id);
    }

    public async Task<Producao> GetByOrdemProducaoId(int id)
    {
        return await _context.Producoes.FirstOrDefaultAsync(x => x.OrdemProducaoId == id);
    }

    public async  Task Update(Producao producao)
    {
        _context.Producoes.Update(producao);
        await _context.SaveChangesAsync();
    }

    public async Task Add(Producao producao)
    {
        // add new producao
        await _context.Producoes.AddAsync(producao);
        // save changes
        await _context.SaveChangesAsync();
    }
}