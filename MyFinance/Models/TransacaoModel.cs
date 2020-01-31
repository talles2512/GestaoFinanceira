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
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a data da transação!")]
        public DateTime Data { get; set; }
        public DateTime DataFinal { get; set; }
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Informe o valor da transação!")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Informe a descrição da transação!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a conta da transação!")]
        public int ContaId { get; set; }
        public string NomeConta { get; set; }

        [Required(ErrorMessage = "Informe o plano de contas da transação!")]
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

        public List<TransacaoModel> EmitirExtrato()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            string data = $"{Data.Year}/{Data.Month}/{Data.Day}";
            string dataFinal = $"{DataFinal.Year}/{DataFinal.Month}/{DataFinal.Day}";

            string query;
            if (Tipo == "A")
            {
                query = "select T.Id, T.Data, T.Tipo, T.Valor, T.Descricao as Historico, T.Conta_Id, C.Nome as Conta, T.PlanoContas_Id, P.Descricao as Plano_Conta" +
                " from Transacao T inner join Conta C on T.Conta_Id = C.Id " +
                "inner join PlanoContas P on T.PlanoContas_Id = P.Id where T.Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado") +
                $" and (T.Data between '{data}' and '{dataFinal}') and T.Conta_Id = {ContaId} order by t.Data desc limit 10";
            }
            else
            {
                query = "select T.Id, T.Data, T.Tipo, T.Valor, T.Descricao as Historico, T.Conta_Id, C.Nome as Conta, T.PlanoContas_Id, P.Descricao as Plano_Conta" +
                " from Transacao T inner join Conta C on T.Conta_Id = C.Id " +
                "inner join PlanoContas P on T.PlanoContas_Id = P.Id where T.Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado") +
                $" and (T.Data between '{data}' and '{dataFinal}') and T.Conta_Id = {ContaId} and T.Tipo = '{Tipo}' order by t.Data desc limit 10";
            }

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

        public void RegistrarTransacao()
        {
            string data = $"{Data.Year}/{Data.Month}/{Data.Day}";
            int id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
            string query = $"insert into Transacao(Data, Tipo, Valor, Descricao, Conta_Id, PlanoContas_Id, Usuario_Id) " +
                $"values('{data}', '{Tipo}', {Valor}, '{Descricao}', {ContaId}, {PlanoContasId}, {id})";
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public void ExcluirTransacao(int id)
        {
            string query = "delete from Transacao where Id =" + id;
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

        public TransacaoModel CarregarRegistro(int? id)
        {
            string query = "select T.Id, T.Data, T.Tipo, T.Valor, T.Descricao as Historico, T.Conta_Id, C.Nome as Conta, T.PlanoContas_Id, P.Descricao as Plano_Conta" +
                " from Transacao T inner join Conta C on T.Conta_Id = C.Id inner join PlanoContas P on T.PlanoContas_Id = P.Id" +
                " where T.Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado") + " and T.Id = " + id;
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            TransacaoModel transacao = new TransacaoModel();
            transacao.Id = int.Parse(dt.Rows[0]["Id"].ToString());
            transacao.Data = DateTime.Parse(dt.Rows[0]["Data"].ToString());
            transacao.Tipo = dt.Rows[0]["Tipo"].ToString();
            transacao.Valor = double.Parse(dt.Rows[0]["Valor"].ToString());
            transacao.Descricao = dt.Rows[0]["Historico"].ToString();
            transacao.ContaId = int.Parse(dt.Rows[0]["Conta_Id"].ToString());
            transacao.NomeConta = dt.Rows[0]["Conta"].ToString();
            transacao.PlanoContasId = int.Parse(dt.Rows[0]["PlanoContas_Id"].ToString());
            transacao.DescricaoPlanoConta = dt.Rows[0]["Plano_Conta"].ToString();

            return transacao;
        }

        public List<PlanoContaModel> CarregarPlanoContas()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();

            string query = "select Id, Descricao from PlanoContas where Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                PlanoContaModel planoConta = new PlanoContaModel();
                planoConta.Id = int.Parse(row["Id"].ToString());
                planoConta.Descricao = row["Descricao"].ToString();
                lista.Add(planoConta);
            }
            return lista;
        }

        public List<ContaModel> CarregarContas()
        {
            List<ContaModel> lista = new List<ContaModel>();

            string query = "select Id, Nome from Conta where Usuario_Id = " + HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                ContaModel conta = new ContaModel();
                conta.Id = int.Parse(row["Id"].ToString());
                conta.Nome = row["Nome"].ToString();
                lista.Add(conta);
            }
            return lista;
        }

        public void AlterarTransacao()
        {
            string data = $"{Data.Year}/{Data.Month}/{Data.Day}";
            int id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
            string query = $"update Transacao set Data = '{data}',Tipo = '{Tipo}',Valor = {Valor}, Descricao = '{Descricao}', Conta_Id = {ContaId}," +
                $" PlanoContas_Id = {PlanoContasId} where Usuario_Id = {id} and Id = {Id}";
            DAL objDAL = new DAL();
            objDAL.NoQuery(query);
        }

    }
}
