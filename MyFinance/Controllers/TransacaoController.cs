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

        [HttpPost]
        public IActionResult CriarTransacao(TransacaoModel transacao)
        {
            if (ModelState.IsValid)
            {
                transacao.HttpContextAccessor = HttpContextAccessor;
                if (transacao.Id != 0)
                {
                    transacao.AlterarTransacao();
                    return RedirectToAction("Index");
                }
                else
                {
                    transacao.RegistrarTransacao();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CriarTransacao(int? id)
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.Contas = obj.CarregarContas();
            ViewBag.PlanoContas = obj.CarregarPlanoContas();

            if (id != null)
            {
                ViewBag.Registro = obj.CarregarRegistro(id);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirTransacao(int id)
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.Registro = obj.CarregarRegistro(id);
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            new TransacaoModel().ExcluirTransacao(id);
            return RedirectToAction("Index");
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