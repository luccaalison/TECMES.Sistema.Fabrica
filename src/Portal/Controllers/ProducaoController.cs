using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers;

[Authorize]
public class ProducaoController : Controller
{
    private readonly ILogger<ProducaoController> _logger;
    private readonly IProducaoRepository _producaoRepository;
    private readonly IOrdemProducaoRepository _ordemProducaoRepository;
    public ProducaoController(ILogger<ProducaoController> logger, IProducaoRepository producaoRepository, IOrdemProducaoRepository ordemProducaoRepository)
    {
        _logger = logger;
        _producaoRepository = producaoRepository;
        _ordemProducaoRepository = ordemProducaoRepository;
    }
    public async Task<ActionResult> Index()
    {
        var producoes = await _producaoRepository.GetAll();
        return View(producoes);
    }
    
    public async Task<ActionResult> OrdensProducoes()
    {
        var ordemProducoes = await _ordemProducaoRepository.GetAll();
        return View(ordemProducoes);
    }
    
    public async Task<ActionResult> CadastrarNovaProducao()
    {
        return View();
    }
    
    [HttpGet("AtualizarOrdem/{id}")]
    public async Task<ActionResult> AtualizarOrdem(int id)
    {
        var ordem = await _ordemProducaoRepository.GetById(id);
        return View(ordem);
    }
    
    [HttpPost("AtualizarOrdem/{id}")]
    public async Task<ActionResult> AtualizarOrdem(int id, [FromForm] OrdemProducao request)
    {
        var ordem = await _ordemProducaoRepository.GetById(id);
        if (ordem == null)
        {
            return NotFound();
        }
        ordem.LiberadoParaProducao = request.LiberadoParaProducao;
        await _ordemProducaoRepository.Update(ordem);
        return RedirectToAction("Index");
    }
    
    [HttpGet("AtualizarProducao/{id}")]
    public async Task<ActionResult> AtualizarProducao(int id)
    {
        var producao = await _producaoRepository.GetById(id);
        return View(producao);
    }
    
    [HttpPost("AtualizarProducao/{id}")]
    public async Task<ActionResult> AtualizarProducao(int id, [FromForm] Producao request)
    {
        var producao = await _producaoRepository.GetById(id);
        if (producao == null)
        {
            return NotFound();
        }
        producao.QuantidadeProduzida = request.QuantidadeProduzida;
        if (producao.OrdemProducao.Quantidade == producao.QuantidadeProduzida)
        {
            producao.ProducaoFinalizada = true;
        }
        await _producaoRepository.Update(producao);
        return RedirectToAction("Index");
    }

    
    [HttpPost]
    public async Task<ActionResult> CadastrarNovaProducao([FromForm] Producao request)
    {
        var ordem = await _ordemProducaoRepository.GetById(request.OrdemProducaoId);
        if (ordem.LiberadoParaProducao == false)
        {
            ModelState.AddModelError("OrdemProducaoId", "A Ordem de Produção não está liberada para produção");
            return View();
        }
        if (ordem.Quantidade < request.Quantidade)
        {
            ModelState.AddModelError("Quantidade", "A quantidade informada é maior que a quantidade da Ordem de Produção");
            return View();
        }
        if (request.Data == DateTime.MinValue)
        {
            ModelState.AddModelError("Data", "O campo Data é obrigatório");
            return View();
        }
        if (request.Quantidade == 0)
        {
            ModelState.AddModelError("Quantidade", "O campo Quantidade é obrigatório");
            return View();
        }
        if (request.OrdemProducaoId == 0)
        {
            ModelState.AddModelError("OrdemProducaoId", "O campo Ordem de Produção é obrigatório");
            return View();
        }
        if (request.MaquinaId == 0)
        {
            ModelState.AddModelError("MaquinaId", "O campo Máquina é obrigatório");
            return View();
        }
        if (request.ProdutoId == 0)
        {
            ModelState.AddModelError("ProdutoId", "O campo Produto é obrigatório");
            return View();
        }
        if (request.OrdemProducaoId == 0)
        {
            ModelState.AddModelError("OrdemProducaoId", "O campo Ordem de Produção é obrigatório");
            return View();
        }
        var novaProducao = new Producao()
        {
            Data = request.Data,
            Quantidade = request.Quantidade,
            OrdemProducaoId = request.OrdemProducaoId,
            ProdutoId = request.ProdutoId,
            MaquinaId = request.MaquinaId
        };
        await _producaoRepository.Add(novaProducao);
        return RedirectToAction("Index");
    }
    
    public async Task<ActionResult> AbrirNovoOrdemDeProducao()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> AbrirNovoOrdemDeProducao([FromForm] OrdemProducao request)
    {
        if (request.Ordem == 0)
        {
            ModelState.AddModelError("Ordem", "O campo Ordem é obrigatório");
            return View();
        }
        if (request.ProdutoId == 0)
        {
            ModelState.AddModelError("ProdutoId", "O campo Produto é obrigatório");
            return View();
        }
        if (request.ClienteId == 0)
        {
            ModelState.AddModelError("ClienteId", "O campo Cliente é obrigatório");
            return View();
        }
        if (request.Quantidade == 0)
        {
            ModelState.AddModelError("Quantidade", "O campo Quantidade é obrigatório");
            return View();
        }
        var novaOrdem = new OrdemProducao()
        {
            Ordem = request.Ordem,
            ProdutoId = request.ProdutoId,
            ClienteId = request.ClienteId,
            Quantidade = request.Quantidade,
            LiberadoParaProducao = false
        };
        await _ordemProducaoRepository.Add(novaOrdem);
        return RedirectToAction("Index");
    }
    
}