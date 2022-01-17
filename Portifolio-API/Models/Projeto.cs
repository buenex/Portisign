using System;

namespace Portifolio_API.Models
{
    public class Projeto{
        public int Id{get;set;}
        public Usuario Usuario{get;set;}
        public string Nome{get;set;}
        public string Descricao{get;set;}
        public string Url{get;set;}
        public string ImgUrl{get;set;}
        public Projeto(){

        }
    }
}