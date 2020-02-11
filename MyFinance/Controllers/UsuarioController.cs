using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? id)
        {
            if(id != null && id == 0)
            {
                HttpContext.Session.Clear();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.RegistrarUsuario();
                return RedirectToAction("Sucesso");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            if (usuario.ValidarLogin())
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", usuario.Nome);
                HttpContext.Session.SetString("IdUsuarioLogado", usuario.Id.ToString());
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                TempData["MensagemLoginInvalido"] = "Dados de login inválidos!";
                return RedirectToAction("Login");
            }
        }

        public IActionResult Sucesso()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Teste(int id)
        {
            TransacaoModel obj = new TransacaoModel();
            ViewBag.Registro = obj.GetTransacao(id);
            return View();
        }
    }
}