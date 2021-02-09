using Microsoft.AspNetCore.Mvc;
using MvcCore.Extensions;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class LoginController : Controller
    {
        IRepository repo;

        public LoginController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(String username, String password)
        {
            Usuario user = this.repo.Login(username, password);
            if (user != null)
            {
                HttpContext.Session.SetObject("usuario", user);
                return RedirectToAction("Index", "Tienda");
            }
            ViewData["mensaje"] = "Error";
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(String nombre, String username, String password)
        {
            this.repo.Insert(nombre, username, password);
            ViewData["mensaje"] = "Datos almacenados";
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
