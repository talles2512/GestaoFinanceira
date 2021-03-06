﻿using System;
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

        [HttpGet]
        public IActionResult Extrato()
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacoes = null;
            ViewBag.Contas = obj.CarregarContas();
            return View();
        }

        [HttpPost]
        public IActionResult Extrato(TransacaoModel transacao)
        {
            transacao.HttpContextAccessor = HttpContextAccessor;
            ViewBag.Contas = transacao.CarregarContas();

            if (transacao.Data != null && transacao.DataFinal != null)
                ViewBag.ListaTransacoes = transacao.EmitirExtrato();
            else
                ViewBag.ListaTransacoes = null;

            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            TransacaoModel obj = new TransacaoModel(HttpContextAccessor);
            ViewBag.Contas = obj.CarregarContas();

            ViewBag.Cores = null;
            ViewBag.Labels = null;
            ViewBag.Valores = null;
            return View();
        }

        [HttpPost]
        public IActionResult Dashboard(TransacaoModel transacao)
        {
            transacao.HttpContextAccessor = HttpContextAccessor;
            ViewBag.Contas = transacao.CarregarContas();

            if (transacao.Data != null && transacao.DataFinal != null)
            {
                Dashboard objDashboard = new Dashboard(HttpContextAccessor);
                string valores = "";
                string labels = "";
                string cores = "";

                var random = new Random();

                foreach (Dashboard dashboard in objDashboard.Grafico(transacao))
                {
                    valores += dashboard.Total + ",";
                    labels += "'" + dashboard.Descricao + "',";
                    cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";
                }

                ViewBag.Cores = cores;
                ViewBag.Labels = labels;
                ViewBag.Valores = valores;
            }
            else
            {
                ViewBag.Cores = null;
                ViewBag.Labels = null;
                ViewBag.Valores = null;
            }
            return View();
        }
    }
}