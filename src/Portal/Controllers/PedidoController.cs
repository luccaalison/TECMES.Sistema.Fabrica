using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Portal.Controllers;

[Authorize]
public class PedidoController : Controller
{
    private readonly ILogger<ProducaoController> _logger;
    private readonly IProducaoRepository _producaoRepository;
    private readonly IOrdemProducaoRepository _ordemProducaoRepository;
    private readonly IPedidoRepository _pedidoRepository;
    
    public PedidoController(ILogger<ProducaoController> logger, IProducaoRepository producaoRepository, IOrdemProducaoRepository ordemProducaoRepository, IPedidoRepository pedidoRepository)
    {
        _logger = logger;
        _producaoRepository = producaoRepository;
        _ordemProducaoRepository = ordemProducaoRepository;
        _pedidoRepository = pedidoRepository;
    }
    
    public async Task<ActionResult> Index()
    {
        var pedidos = await _pedidoRepository.GetAll();
        return View(pedidos);
    }
    
    public async Task<ActionResult> CadastrarPedido()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> CadastrarPedido([FromForm] Pedido request)
    {
        if (request.ClienteId == 0)
        {
            ModelState.AddModelError("ClienteId", "Cliente não encontrado");
            return View();
        }
        if (request.ProdutoId == 0)
        {
            ModelState.AddModelError("ProdutoId", "Produto não encontrado");
            return View();
        }
        if (request.Quantidade == 0)
        {
            ModelState.AddModelError("Quantidade", "Quantidade não pode ser 0");
            return View();
        }
        if (request.OrdemDeProducaoId == 0)
        {
            ModelState.AddModelError("OrdemDeProducaoId", "Ordem de produção não encontrada");
            return View();
        }
        if (request.Data == DateTime.MinValue)
        {
            ModelState.AddModelError("Data", "Data não pode ser vazia");
            return View();
        }
        if (request.CodigoDaVenda == 0)
        {
            ModelState.AddModelError("CodigoDaVenda", "Código da venda não pode ser 0");
            return View();
        }
        
        // valid ordem de producao
        var ordem = await _ordemProducaoRepository.GetById(request.OrdemDeProducaoId);
        if (ordem == null)
        {
            ModelState.AddModelError("OrdemDeProducaoId", "Ordem de produção não encontrada");
            return View();
        }
        var producao = await _producaoRepository.GetByOrdemProducaoId(ordem.Id);
        if (producao.ProducaoFinalizada)
        {
            request.VendaFinalizada = true;
        }
        else
        {
            request.VendaFinalizada = false;
        }
        if (request.Quantidade > producao.Quantidade)
        {
            ModelState.AddModelError("Quantidade", "Quantidade maior que a quantidade da ordem de produção");
            return View();
        }
        
        // add new pedido
        var novoPedido = new Pedido()
        {
            ClienteId = request.ClienteId,
            ProdutoId = request.ProdutoId,
            Quantidade = request.Quantidade,
            OrdemDeProducaoId = request.OrdemDeProducaoId,
            Data = request.Data,
            CodigoDaVenda = request.CodigoDaVenda,
            VendaFinalizada = request.VendaFinalizada
        };
        
        await _pedidoRepository.Create(novoPedido);
        return RedirectToAction("Index");
    }
    
    [HttpGet("AtualizarPedido/{id}")]
    public async Task<ActionResult> LiberarPedido(int id)
    {
        var pedido = await _pedidoRepository.GetById(id);
        return View(pedido);
    }
    
    [HttpPost("AtualizarPedido/{id}")]
    public async Task<ActionResult> LiberarPedido(int id, [FromForm] Pedido request)
    {
        var pedido = await _pedidoRepository.GetById(id);
        if (pedido == null)
        {
            return NotFound();
        }
        
        var ordem = await _ordemProducaoRepository.GetById(pedido.OrdemDeProducaoId);
        var producao = await _producaoRepository.GetByOrdemProducaoId(ordem.Id);
        if (!producao.ProducaoFinalizada)
        {
            ModelState.AddModelError("VendaFinalizada", "Produção não finalizada");
            return View();
        }
        pedido.VendaFinalizada = request.VendaFinalizada;
        await _pedidoRepository.Update(pedido);
        return RedirectToAction("Index");
    }
}