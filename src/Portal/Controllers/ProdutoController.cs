using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers;

[Authorize]
public class ProdutoController : Controller
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(ILogger<ProdutoController> logger, IProdutoRepository produtoRepository)
    {
        _logger = logger;
        _produtoRepository = produtoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoRepository.GetAll();
        return View(produtos);
    }
}