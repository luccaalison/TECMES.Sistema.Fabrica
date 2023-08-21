namespace Domain;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public int OrdemDeProducaoId { get; set; }
    public DateTime Data { get; set; }
    public int CodigoDaVenda { get; set; }
    public bool VendaFinalizada { get; set; }
    public virtual Cliente Cliente { get; set; }
    public virtual Produto Produto { get; set; }
    public virtual OrdemProducao OrdemDeProducao { get; set; }
    
}