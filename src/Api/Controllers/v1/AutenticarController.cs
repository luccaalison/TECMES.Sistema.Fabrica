using Api.Models;
using Api.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AutenticarController : ControllerBase
{
    
    private readonly IUsuarioRepository _usuarioRepo;

    public AutenticarController(IUsuarioRepository usuarioRepo)
    {
        _usuarioRepo = usuarioRepo;
    }

    /// <summary>
    /// Autenticar Usuário e Gerar Token
    /// </summary>
    /// <param name="usuarioService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/Autenticacao/login
    /// </remarks>
    /// <returns> Usuário Logado com Sucesso </returns>
    /// <response code="200">Usuário Logado com Sucesso</response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [HttpPost("login")]
    [ApiExplorerSettings(GroupName = "Autenticacao")]
    [ProducesResponseType(typeof(UsuarioLogadoResponse), 200)]
    [ProducesResponseType(typeof(UsuarioLogadoResponse), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> AutenticarUsuario(
        [FromBody] AutenticarUsuarioRequest request
    )
    {
        try
        {
            var usuario = await _usuarioRepo.GetByLogin(request.Login);
            var usuarioResponse = new UsuarioLogadoResponse();
            if (usuario == null)
            {
                return StatusCode(401, "Usuário não encontrado");
            }
            if (usuario.Senha != request.Senha)
            {
                return StatusCode(401, "Senha incorreta");
            }
            usuarioResponse.Nome = usuario.Nome;
            usuarioResponse.Login = usuario.Login;
            usuarioResponse.Id = usuario.Id;
            
            var jwtToken = await new AutenticaoService().GenerateToken(usuarioResponse);
            usuarioResponse.Token = jwtToken;
            return StatusCode(200, usuarioResponse);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Validar se o Usuário está Logado
    /// </summary>
    /// <remarks>
    /// POST /api/v1/Autenticacao/autenticado
    /// </remarks>
    /// <returns> Nome do Usuário </returns>
    /// <response code="200">Nome do Usuário</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [HttpGet("autenticado")]
    [ApiExplorerSettings(GroupName = "Autenticacao")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [Authorize]
    public IActionResult Authenticated()
    {
        try
        {
            return StatusCode(200, User.Identity.Name);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }    
    }
}