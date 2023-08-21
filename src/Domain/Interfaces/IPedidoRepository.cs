namespace Domain.Interfaces;

public interface IPedidoRepository
{
    #region CRUD
    Task<Pedido> GetById(int id);
    Task<List<Pedido>> GetAll();
    Task Create(Pedido pedido);
    Task Update(Pedido pedido);
    Task<Pedido> Delete(int id);
    #endregion
}