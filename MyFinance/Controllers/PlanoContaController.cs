using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            PlanoContaModel obj = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListaPlanoContas = obj.ListaPlanoContas();
            return View();
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoConta)
        {
            if (ModelState.IsValid)
            {
                planoConta.HttpContextAccessor = HttpContextAccessor;
                if (planoConta.Id != 0)
                {
                    planoConta.AlterarPlanoConta();
                    return RedirectToAction("Index");
                }
                else
                {
                    planoConta.RegistrarPlanoConta();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if (id != null)
            {
                PlanoContaModel obj = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = obj.CarregarRegistro(id);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirPlanoConta(int id)
        {
            new PlanoContaModel().ExcluirPlanoConta(id);
            return RedirectToAction("Index");
        }
    }
}