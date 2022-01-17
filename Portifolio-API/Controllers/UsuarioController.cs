using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portifolio_API.Models;
using Portifolio_API.Utils;
using static Portifolio_API.Data.UsuarioData;

namespace Portifolio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Usuario> getAll(){
            return getAllUsuario();
        }

        [Route("getById")]
        [HttpGet]
        public Usuario getById(int id){            
            return getUsuarioById(id);
        }

        [Route("getByUsuario")]
        [HttpGet]
        public Usuario getByUsuario(string usuario){            
            return getUsuarioByUsuario(usuario);
        }

        [Route("getTokenByEmail")]
        [HttpGet]
        public string getTokenByEmail(string email){            
            return getUsuarioEmail(email).Token;
        }

        [Route("login")]
        [HttpPost]
        public bool getLogin([FromBody]Login credenciais){            
            return login(credenciais.Usuario,credenciais.Senha);
        }

        [Route("insert")]
        [HttpPost]
        public bool insert([FromBody]Usuario user){      
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE");
            Response.Headers.Add("Access-Control-Max-Age", "3600");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Origin, Cache-Control, X-Requested-With");
            Response.Headers.Add("Access-Control-Allow-Credentials", "true");     

            return insertUsuario(user);
        }

        [Route("sendEmail")]
        [HttpGet]
        public void sendEmail(string email){   
            string subject = "Recuperação de senha";   
            string body = "<h1>Recuperação de senha Portisign</h1><br>";
            string token = getUsuarioEmail(email).Token;
            body+= "<p>Segue o link para alteração da sua senha, lembrando que quando sua senha for alterada, por motivos de segurança alteraremos seu token</p>";
            body+= "Link: <a href='http://localhost:8081/pages/alterarSenha?token="+token+"'>Alteração de senha</a>";

            if(token != "")
                Mail.sendMessage(email,subject,body,true);
        }
    }
}