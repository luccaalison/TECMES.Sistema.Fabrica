namespace Domain;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Teste { get; set; }
    public virtual List<OrdemProducao> OrdensProducao { get; set; }
    public virtual List<Pedido> Pedidos { get; set; }
}