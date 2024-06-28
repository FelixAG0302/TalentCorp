using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Entities;

namespace TalentCorp.Controllers.Authentication;

public class AuthenticationController(TalentCorpContext context) : Controller
{
    private const string DefaultRole = "USER";

    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registro(Usuario modelUsuario)
    {
        var usuario = new Usuario
        {
            Username = modelUsuario.Username,
            Email = modelUsuario.Email,
            Password = modelUsuario.Password
        };

        var rol = await context.Roles
            .Where(rol => DefaultRole.Equals(rol.Nombre))
            .FirstOrDefaultAsync() ?? new Role
        {
            Nombre = "USER",
            Descripcion = "Default user permissions."
        };

        await context.UsuariosRoles.AddAsync(
            new UsuariosRole
            {
                User = usuario,
                Role = rol
            }
        );

        await context.Usuarios.AddAsync(usuario);
        await context.SaveChangesAsync();

        return RedirectToAction("Login", "Authentication");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Usuario modelUsuario)
    {
        var usuario = await context.Usuarios
            .Where(u => u.Username == modelUsuario.Username && u.Password == modelUsuario.Password)
            .FirstOrDefaultAsync();
        if (usuario == null)
        {
            ViewData["Mensaje"] = "¡Las credenciales son incorrectas!";
            return View(modelUsuario);
        }

        Response.Cookies.Append(
            "Authorization",
            "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Username + ":" + usuario.Password)),
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1),
                HttpOnly = true
            }
        );

        return RedirectToAction("Index", "Home");
    }
}