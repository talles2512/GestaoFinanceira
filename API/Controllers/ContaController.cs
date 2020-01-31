using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        [HttpGet]
        public ContaModel Get(int id)
        {
            ContaModel contaModel = new ContaModel();
            return contaModel.GetConta(id);
        }
    }
}