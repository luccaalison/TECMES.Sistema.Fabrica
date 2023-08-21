using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AplicacaoContext _context;

    public PedidoRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<Pedido> GetById(int id)
    {
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task<List<Pedido>> GetAll()
    {
        return await _context.Pedidos.ToListAsync();
    }

    public async Task Create(Pedido pedido)
    {
        // add
        await _context.Pedidos.AddAsync(pedido);
        // save changes
        await _context.SaveChangesAsync(); 
    }

    public async Task Update(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<Pedido> Delete(int id)
    {
        throw new NotImplementedException();
    }
}