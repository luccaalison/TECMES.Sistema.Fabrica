using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class OrdemProducaoRepository : IOrdemProducaoRepository
{
    private readonly AplicacaoContext _context;

    public OrdemProducaoRepository(AplicacaoContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<OrdemProducao>> GetAll()
    {
        return await _context.OrdensProducao.ToListAsync();
    }

    public async Task<OrdemProducao> GetById(int id)
    {
        return await _context.OrdensProducao.FirstAsync(x => x.Id == id);
    }

    public async Task Update(OrdemProducao ordemProducao)
    {
        _context.OrdensProducao.Update(ordemProducao);
        await _context.SaveChangesAsync();
    }
    
    public async Task Add(OrdemProducao ordemProducao)
    {
        await _context.OrdensProducao.AddAsync(ordemProducao);
        await _context.SaveChangesAsync();
    }
}