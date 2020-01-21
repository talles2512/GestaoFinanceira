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
    public class PlanoContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a descrição do plano de conta!")]
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int UsuarioId { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaModel()
        {

        }

        //Recebe o contexto para acesso às variáveis de sessão.
        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<PlanoContaModel> ListaPlanoContas()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();

            string query = "select Id, Descricao, Tipo, Usuario_Id from PlanoContas where Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                PlanoContaModel planoConta = new PlanoContaModel();
                planoConta.Id = int.Parse(row["Id"].ToString());
                planoConta.Descricao = row["Descricao"].ToString();
                planoConta.Tipo = row["Tipo"].ToString();
                planoConta.UsuarioId = int.Parse(row["Usuario_Id"].ToString());
                lista.Add(planoConta);
            }
            return lista;
        }

        public void RegistrarPlanoConta()
        {
            int id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
            string query = $"insert into PlanoContas(Descricao,Tipo,Usuario_Id)" +
                $"values('{Descricao}','{Tipo}',{id})";
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public void ExcluirPlanoConta(int id)
        {
            string query = "delete from PlanoContas where Id =" + id;
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public PlanoContaModel CarregarRegistro(int? id)
        {
            string query = "select Id, Descricao, Tipo, Usuario_Id from PlanoContas where Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado")
                + " and Id = " + id;
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            PlanoContaModel planoConta = new PlanoContaModel();
            planoConta.Id = int.Parse(dt.Rows[0]["Id"].ToString());
            planoConta.Descricao = dt.Rows[0]["Descricao"].ToString();
            planoConta.Tipo = dt.Rows[0]["Tipo"].ToString();
            planoConta.UsuarioId = int.Parse(dt.Rows[0]["Usuario_Id"].ToString());

            return planoConta;
        }

        public void AlterarPlanoConta()
        {
            int id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
            string query = $"update PlanoContas set Descricao = '{Descricao}',Tipo = '{Tipo}' where Usuario_Id = {id} and Id = {Id}";
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }
    }
}
