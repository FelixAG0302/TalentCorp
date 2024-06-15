using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TalentCorp.Models;
using TalentCorp.Models.DB;

namespace TalentCorp.Controllers
{
    public class LoginController : Controller
    {

        private readonly TalentCorpContext _appDbContext;
        public LoginController(TalentCorpContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
       

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(User modelUsuario)
        {
            User usuario = new User()
            {
                UserName = modelUsuario.UserName,
                Email = modelUsuario.Email,
                Password = modelUsuario.Password
            };

            await _appDbContext.Users.AddAsync(usuario);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Login", "Login");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User modelUsuario)
        {
          User? usuario = await _appDbContext.Users
                                        .Where(u => u.UserName == modelUsuario.UserName && u.Password == modelUsuario.Password)
                                        .FirstOrDefaultAsync();
                if (usuario == null)
                {
                    ViewData["Mensaje"] = "El usuario ingresado es incorrecto";
                    return View(modelUsuario);
                }

               return RedirectToAction("Index", "Home");
        }


    }
}
