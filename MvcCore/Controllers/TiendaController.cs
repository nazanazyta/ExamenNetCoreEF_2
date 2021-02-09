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
    public class TiendaController : Controller
    {
        IRepository repo;

        public TiendaController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                List<int> deptsession;
                if (HttpContext.Session.GetObject<List<int>>("deptsession") == null)
                {
                    deptsession = new List<int>();
                }
                else
                {
                    deptsession = HttpContext.Session.GetObject<List<int>>("deptsession");
                }
                if (deptsession.Contains(id.Value) == false)
                {
                    deptsession.Add(id.GetValueOrDefault());
                    HttpContext.Session.SetObject("deptsession", deptsession);
                }
                ViewData["mensaje"] = "Datos almacenados: " + deptsession.Count;
            }
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Carrito(int? eliminar)
        {
            List<int> deptsession = HttpContext.Session.GetObject<List<int>>("deptsession");
            if (deptsession == null)
            {
                return View();
            }
            else
            {
                if (eliminar != null)
                {
                    deptsession.Remove(eliminar.Value);
                    HttpContext.Session.SetObject("deptsession", deptsession);
                }
                List<Departamento> departamentos = new List<Departamento>();
                foreach (int id in deptsession)
                {
                    Departamento dept = this.repo.BuscarDepartamentoId(id);
                    departamentos.Add(dept);
                }
                return View(departamentos);
            }
        }

        [HttpPost]
        public IActionResult Carrito(List<int> cantidades)
        {
            List<int> deptsession = HttpContext.Session.GetObject<List<int>>("deptsession");
            List<Departamento> departamentos = new List<Departamento>();
            foreach (int id in deptsession)
            {
                Departamento dept = this.repo.BuscarDepartamentoId(id);
                departamentos.Add(dept);
            }
            TempData.SetObjectTempData("departamentos", departamentos);
            TempData.SetObjectTempData("cantidades", cantidades);
            return RedirectToAction("Pedidos");
        }

        public IActionResult Pedidos()
        {
            List<int> cantidades = TempData.GetObjectTempData<List<int>>("cantidades");
            List<Departamento> departamentos = TempData.GetObjectTempData<List<Departamento>>("departamentos");
            ViewData["cantidades"] = cantidades;
            return View(departamentos);
        }
    }
}
