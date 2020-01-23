﻿using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a data da transação!")]
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Informe o valor da transação!")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Informe a descrição da transação!")]
        public string Descricao { get; set; }
        
        public int ContaId { get; set; }
        public string NomeConta { get; set; }
        public int PlanoContasId { get; set; }
        public string DescricaoPlanoConta { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel()
        {

        }

        //Recebe o contexto para acesso às variáveis de sessão.
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<TransacaoModel> ListaTransacoes()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();

            string query = "select T.Id, T.Data, T.Tipo, T.Valor, T.Descricao as Historico, T.Conta_Id, C.Nome as Conta, T.PlanoContas_Id, P.Descricao as Plano_Conta" +
                " from Transacao T inner join Conta C on T.Conta_Id = C.Id " +
                "inner join PlanoContas P on T.PlanoContas_Id = P.Id where T.Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado") +
                " order by t.Data desc limit 10";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                TransacaoModel transacao = new TransacaoModel();
                transacao.Id = int.Parse(row["Id"].ToString());
                transacao.Data = DateTime.Parse(row["Data"].ToString());
                transacao.Tipo = row["Tipo"].ToString();
                transacao.Valor = double.Parse(row["Valor"].ToString());
                transacao.Descricao = row["Historico"].ToString();
                transacao.ContaId = int.Parse(row["Conta_Id"].ToString());
                transacao.NomeConta = row["Conta"].ToString();
                transacao.PlanoContasId = int.Parse(row["PlanoContas_Id"].ToString());
                transacao.DescricaoPlanoConta = row["Plano_Conta"].ToString();
                lista.Add(transacao);
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
