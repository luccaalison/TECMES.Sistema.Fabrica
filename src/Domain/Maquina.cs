namespace Domain;

public class Maquina
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public virtual List<Producao> Producoes { get; set; }
}