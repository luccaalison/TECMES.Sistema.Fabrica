namespace Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAll();
    Task<Produto> GetById(int id);
}