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
using Microsoft.AspNetCore.Cors;
using static Portifolio_API.Utils.Cryptography;
using static Portifolio_API.Data.ProjetoData;

namespace Portifolio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Projeto> getAll(){
            return getAllProjetos();
        }

        [Route("getById")]
        [HttpGet]
        public Projeto getById(int id){
            return getProjetoById(id);
        }

        [Route("getByUsuario")]
        [HttpGet]
        public IEnumerable<Projeto> getByUsuario(string usuario){
            return getProjetoByUsuario(usuario);
        }

        [Route("getByTokenPage")]
        [HttpGet]
        public IEnumerable<Projeto> getByTokenPage(string tokenPage){
            return getProjetoByTokenPage(tokenPage);
        }

        [EnableCors()]
        [Route("getByToken")]
        [HttpGet]
        public IEnumerable<Projeto> getByToken(string token){
            return getProjetoByToken(token);
        }

        // [Route("getByToken")]
        // [HttpGet]
        // public string getByToken(string token){
        //     return token;
        // }
    }
}