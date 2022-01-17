using System;

namespace Portifolio_API.Models
{
    public class Usuario{
        public int Id{get;set;}
        public Pessoa Pessoa{get;set;}
        public string usuario{get;set;}
        public string Senha{get;set;}
        public string DicaSenha{get;set;}
        public string Token{get;set;}
        public Usuario(){

        }
    }
}