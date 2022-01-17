using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portifolio_API.Models;
using Portifolio_API.Data;
using static Portifolio_API.Data.PessoaData;

namespace Portifolio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Pessoa> getAll(){
            return getAllPessoa();
        }

        [Route("getById")]
        [HttpGet]
        public Pessoa getById(int id){
            return getPessoaById(id);
        }
    }
}