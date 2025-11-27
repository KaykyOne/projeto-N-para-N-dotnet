using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudNpN.Models;
using CrudNpN.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudNpN.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly AppDbContext _context;

    public LoginController(ILogger<LoginController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Usuario usuario)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Email ou senha inválidos.";
        }

        var userDb = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Email == usuario.Email && m.Senha == usuario.Senha);

        if (userDb == null)
        {
            TempData["ErrorMessage"] = "Email ou senha inválidos.";
            return RedirectToAction("Index", "Login");
        }

        // login bem-sucedido, vai pra Home
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro([Bind("Nome,Email,Senha")] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        // Verifica se email já existe
        var emailExiste = await _context.Usuarios
            .AnyAsync(u => u.Email == usuario.Email);

        if (emailExiste)
        {
            TempData["ErrorMessage"] = "Este email já está cadastrado.";
            return View(usuario);
        }

        try
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cadastro realizado com sucesso! Faça seu login.";
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Erro ao realizar cadastro. Tente novamente.";
            return View(usuario);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
