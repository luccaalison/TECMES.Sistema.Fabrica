using System.Security.Claims;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(new Usuario());
    }
    
    public IActionResult Logout()  
    {  
        var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  
        return RedirectToAction("Index");  
    } 
    
    [HttpPost]
    public async Task<IActionResult> Index([FromForm] Usuario request)
    {
        if (request == null)
        {
            ModelState.AddModelError("Login", "Preencha o login");
            return View();
        }
        if (string.IsNullOrEmpty(request.Login))
        {
            ModelState.AddModelError("Login", "Preencha o login");
            return View();
        }
        if (string.IsNullOrEmpty(request.Senha))
        {
            ModelState.AddModelError("Senha", "Preencha a senha");
            return View();
        }
        var usuario = await _usuarioRepository.GetByLogin(request.Login);
        if (usuario == null)
        {
            ModelState.AddModelError("Login", "Usuário não encontrado");
            return View();
        }
        if (usuario.Senha != request.Senha)
        {
            ModelState.AddModelError("Senha", "Senha incorreta");
            return View();
        }
        var identity = new ClaimsIdentity(new[] {  
            new Claim(ClaimTypes.Name, usuario.Login)  
        }, CookieAuthenticationDefaults.AuthenticationScheme);  
  
        var principal = new ClaimsPrincipal(identity);  
  
        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return RedirectToAction("Index", "Home");
    }
}