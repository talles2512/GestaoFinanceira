using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacoes = obj.ListaTransacoes();
            return View();
        }

        [HttpGet]
        public IActionResult CriarTransacao(int? id, int? contaId, int? planoContasId)
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.Contas = obj.CarregarContas();
            ViewBag.PlanoContas = obj.CarregarPlanoContas();

            if (id != null && contaId != null && planoContasId != null)
            {
                ViewBag.Registro = obj.CarregarRegistro(id, contaId, planoContasId);
            }
            return View();
        }

        public IActionResult Extrato()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}