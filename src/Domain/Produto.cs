namespace Domain;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public virtual List<OrdemProducao> OrdensProducao { get; set; }
    public virtual List<Producao> Producoes { get; set; }
    public virtual List<Pedido> Pedidos { get; set; }
}