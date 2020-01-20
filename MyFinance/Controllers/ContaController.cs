using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            ContaModel obj = new ContaModel(HttpContextAccessor);
            ViewBag.ListaConta = obj.ListaContas();
            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel conta)
        {
            if (ModelState.IsValid)
            {
                conta.HttpContextAccessor = HttpContextAccessor;
                conta.RegistrarConta();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirConta(int id)
        {
            new ContaModel().ExcluirConta(id);
            return RedirectToAction("Index");
        }
    }
}