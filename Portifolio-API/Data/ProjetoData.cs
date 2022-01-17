using System;
using System.Data.SqlClient;
using Portifolio_API.Models;
using System.Text;
using System.Data;
using System.Collections.Generic;
using static Portifolio_API.Data.Data;

namespace Portifolio_API.Data{
    public class ProjetoData{
        public static List<Projeto> getAllProjetos(){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Projeto");

            return getObjs<Projeto>(query.ToString(),parameters,extractObject);
        }

        public static Projeto getProjetoById(int id){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Projeto ");
            query.Append("WHERE id = @id");
            
            SqlParameter parameter = new SqlParameter("@id",SqlDbType.Int);
            parameter.Value = id;
            parameters.Add(parameter);

            return getObj<Projeto>(query.ToString(),parameters,extractObject);
        }

        public static List<Projeto> getProjetoByUsuario(string usuario){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Projeto P ");
            query.Append("INNER JOIN Usuario U ");
            query.Append("ON U.usuario = @usuario ");
            query.Append("AND U.id = P.id_usuario ");
            
            SqlParameter parameter = new SqlParameter("@usuario",SqlDbType.VarChar);
            parameter.Value = usuario;
            parameters.Add(parameter);

            return getObjs<Projeto>(query.ToString(),parameters,extractObject);
        }

        public static List<Projeto> getProjetoByToken(string token){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT P.* FROM Projeto P ");
            query.Append("INNER JOIN Usuario U ");
            query.Append("ON U.token = @token ");
            query.Append("AND U.id = P.id_usuario ");
            
            SqlParameter parameter = new SqlParameter("@token",SqlDbType.VarChar,int.MaxValue);
            parameter.Value = token;
            parameters.Add(parameter);

            return getObjs<Projeto>(query.ToString(),parameters,extractObject);
        }

        public static List<Projeto> getProjetoByTokenPage(string tokenPage){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT P.* FROM Projeto P ");
            query.Append("INNER JOIN Usuario U ");
            query.Append("ON U.token_page = @token ");
            query.Append("AND U.id = P.id_usuario ");
            
            SqlParameter parameter = new SqlParameter("@token",SqlDbType.VarChar,int.MaxValue);
            parameter.Value = tokenPage;
            parameters.Add(parameter);

            return getObjs<Projeto>(query.ToString(),parameters,extractObject);
        }

        static Projeto extractObject(SqlDataReader reader){
            Projeto projeto = new Projeto();

            projeto.Id = (int)reader["id"];
            projeto.Usuario = UsuarioData.getUsuarioById((int)reader["id_usuario"]);
            projeto.Nome = (string)reader["nome"];
            projeto.Descricao = (string)reader["descricao"];
            projeto.ImgUrl = (string)reader["img_url"];
            projeto.Url = (string)reader["url"];

            return projeto;
        }
    }
}