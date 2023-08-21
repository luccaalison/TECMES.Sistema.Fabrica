using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    
    /// <summary>
    /// Obter todos os produtos
    /// </summary>
    /// <returns></returns>
    [HttpPost("all")]
    [ProducesResponseType(typeof(List<Produto>), 200)]
    [ProducesResponseType(typeof(List<Produto>), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> All()
    {
        var listaPropdutos = await _produtoRepository.GetAll();
        List<Produto> produtos = new List<Produto>();
        if (listaPropdutos != null) 
        {
            foreach (var prod in listaPropdutos)
            {
                produtos.Add(new Produto()
                {
                    Id = prod.Id,
                    Nome = prod.Nome
                });
            }   
        }
        
        return StatusCode(200, produtos);
    }
    
    /// <summary>
    /// Obter produto por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var produto = await _produtoRepository.GetById(id);
        return StatusCode(200, produto);
    }
}