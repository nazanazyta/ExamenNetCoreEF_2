using Microsoft.AspNetCore.Mvc;
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
            return View(user);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(String nombre, String username, String pass)
        {
            this.repo.Insert(nombre, username, pass);
            return View();
        }
    }
}
