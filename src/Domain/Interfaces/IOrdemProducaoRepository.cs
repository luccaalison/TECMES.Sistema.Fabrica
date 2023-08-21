namespace Domain.Interfaces;

public interface IOrdemProducaoRepository
{
    Task<IEnumerable<OrdemProducao>> GetAll();
    Task<OrdemProducao> GetById(int id);
    Task Update(OrdemProducao ordemProducao);
    Task Add(OrdemProducao ordemProducao);
}