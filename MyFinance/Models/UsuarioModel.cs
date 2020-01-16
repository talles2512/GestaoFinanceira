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

        public bool ValidarLogin()
        {
            string query = $"select Id, Nome, Data_Nascimento from Usuario where Email = '{Email}' and Senha = '{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.Reader(query);

            if(dt != null)
            {
                if(dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["Id"].ToString());
                    Nome = dt.Rows[0]["Nome"].ToString();
                    DataNascimento = DateTime.Parse(dt.Rows[0]["Data_Nascimento"].ToString());
                    return true;
                }
            }

            return false;
        }
    }
}
