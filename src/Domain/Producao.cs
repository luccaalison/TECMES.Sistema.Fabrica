namespace Domain;

public class Producao
{
    public int Id { get; set; }
    public int MaquinaId { get; set; }
    public int ProdutoId { get; set; }
    public int OrdemProducaoId { get; set; }
    public int Quantidade { get; set; }
    public int QuantidadeProduzida { get; set; }
    public int Sequencia { get; set; }
    public bool ProducaoFinalizada { get; set; }
    public DateTime Data { get; set; }
    public virtual Maquina Maquina { get; set; }
    public virtual Produto Produto { get; set; }
    public virtual OrdemProducao OrdemProducao { get; set; }
}