using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

        public UsuarioModel()
        {
        }
        public UsuarioModel(int id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public bool ValidarLogin()
        {
            string query = $"select Id, Nome, Data_Nascimento from Usuario where Email = '{Email}' and Senha = '{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);

            if(dt != null)
            {
                if(dt.Rows.Count == 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
