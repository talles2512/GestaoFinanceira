using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da conta!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o saldo inicial da conta!")]
        public double Saldo { get; set; }
        public int UsuarioId { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ContaModel()
        {

        }

        //Recebe o contexto para acesso às variáveis de sessão.
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<ContaModel> ListaContas()
        {
            List<ContaModel> lista = new List<ContaModel>();

            string query = "select Id, Nome, Saldo, Usuario_Id from Conta where Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                ContaModel conta = new ContaModel();
                conta.Id = int.Parse(row["Id"].ToString());
                conta.Nome = row["Nome"].ToString();
                conta.Saldo = double.Parse(row["Saldo"].ToString());
                conta.UsuarioId = int.Parse(row["Usuario_Id"].ToString());
                lista.Add(conta);
            }
            return lista;
        }

        public void RegistrarConta()
        {
            int id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
            string query = $"insert into Conta(Nome,Saldo,Usuario_Id)" +
                $"values('{Nome}','{Saldo}',{id})";
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public void ExcluirConta(int id)
        {
            string query = "delete from Conta where Id =" + id;
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public ContaModel GetConta(int id)
        {
            string query = "select Id, Nome, Saldo, Usuario_Id from Conta where Usuario_Id = 1 and Id = " + id;
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            ContaModel conta = new ContaModel();
            conta.Id = int.Parse(dt.Rows[0]["Id"].ToString());
            conta.Nome = dt.Rows[0]["Nome"].ToString();
            conta.Saldo = double.Parse(dt.Rows[0]["Saldo"].ToString());
            conta.UsuarioId = int.Parse(dt.Rows[0]["Usuario_Id"].ToString());
            return conta;
        }
    }
}
