namespace Domain;

public class OrdemProducao
{
    public int Id { get; set; }
    public int Ordem { get; set; }
    public int Quantidade { get; set; }
    public int ClienteId { get; set; }
    public int ProdutoId { get; set; }
    public bool LiberadoParaProducao { get; set; }
    public virtual Produto Produto { get; set; }
    public virtual Cliente Cliente { get; set; }
    public virtual Producao Producao { get; set; }
    public virtual Pedido Pedido { get; set; }
}