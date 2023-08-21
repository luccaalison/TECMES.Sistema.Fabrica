namespace Api.Models;

public class UsuarioLogadoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Token { get; set; }
}