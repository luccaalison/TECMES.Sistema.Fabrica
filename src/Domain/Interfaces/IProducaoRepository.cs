namespace Domain.Interfaces;

public interface IProducaoRepository
{
    Task<IEnumerable<Producao>> GetAll();
    Task<Producao> GetById(int id);
    Task<Producao> GetByOrdemProducaoId(int id);
    Task Update(Producao producao);
    Task Add(Producao producao);
}