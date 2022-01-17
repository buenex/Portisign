using System;
using System.Data.SqlClient;
using Portifolio_API.Models;
using System.Text;
using System.Data;
using System.Collections.Generic;
using static Portifolio_API.Data.Data;
using static Portifolio_API.Data.PessoaData;

namespace Portifolio_API.Data{
    public class UsuarioData{
        public static List<Usuario> getAllUsuario(){
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM Usuario ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            return getObjs<Usuario>(query.ToString(),parameters,extractObject);            
        }

        public static Usuario getUsuarioById(int id){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Usuario ");
            query.Append("WHERE id = @id");

            SqlParameter parameter = new SqlParameter("@id",SqlDbType.Int);
            parameter.Value = id;
            parameters.Add(parameter);

            return getObj<Usuario>(query.ToString(),parameters,extractObject); 
        }

        public static Usuario getUsuarioByUsuario(string usuario){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Usuario ");
            query.Append("WHERE usuario = @usuario");

            SqlParameter parameter = new SqlParameter("@usuario",SqlDbType.VarChar,50);
            parameter.Value = usuario;
            parameters.Add(parameter);

            return getObj<Usuario>(query.ToString(),parameters,extractObject); 
        }

        public static Usuario getUsuarioEmail(string email){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT U.* FROM Usuario U ");
            query.Append("INNER JOIN Pessoa P ON U.id_pessoa = P.id ");
            query.Append("WHERE P.email = @email ");

            SqlParameter parameter = new SqlParameter("@email",SqlDbType.VarChar,200);
            parameter.Value = email;
            parameters.Add(parameter);

            return getObj<Usuario>(query.ToString(),parameters,extractObject); 
        }

        public static bool login(string usuario,string senha){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.Append("SELECT * FROM Usuario ");
            query.Append("WHERE usuario = @usuario ");
            query.Append("AND senha = @senha ");

            SqlParameter param_usuario = new SqlParameter("@usuario",SqlDbType.VarChar,50);
            SqlParameter param_senha = new SqlParameter("@senha",SqlDbType.VarChar,50);
            
            param_usuario.Value = usuario;
            param_senha.Value = senha;

            parameters.Add(param_usuario);
            parameters.Add(param_senha);

            return hasRows(query.ToString(),parameters); 
        }

        public static bool insertUsuario(Usuario usuario){
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            if(!insertPessoa(usuario.Pessoa))
                return false;
            else
                usuario.Pessoa = getPessoaByCpf(usuario.Pessoa.Cpf);

            query.Append("INSERT INTO Usuario ");
            query.Append("VALUES (@id_pessoa,@usuario,@senha,@dica_senha,@token)");            

            SqlParameter param_id_pessoa = new SqlParameter("@id_pessoa",SqlDbType.Int);
            SqlParameter param_usuario = new SqlParameter("@usuario",SqlDbType.VarChar,50);
            SqlParameter param_senha = new SqlParameter("@senha",SqlDbType.VarChar,50);
            SqlParameter param_dica_senha = new SqlParameter("@dica_senha",SqlDbType.VarChar,50);
            SqlParameter param_token = new SqlParameter("@token",SqlDbType.VarChar,50);
            
            param_id_pessoa.Value = usuario.Pessoa.Id;
            param_usuario.Value = usuario.usuario;
            param_senha.Value = usuario.Senha;
            param_dica_senha.Value = usuario.DicaSenha;
            param_token.Value = usuario.Token;

            parameters.Add(param_id_pessoa);
            parameters.Add(param_usuario);
            parameters.Add(param_senha);
            parameters.Add(param_dica_senha);
            parameters.Add(param_token);

            return executeNonQuery(query.ToString(),parameters);
        }

        static Usuario extractObject(SqlDataReader reader){
            Usuario usuario = new Usuario();

            usuario.Id = (int)reader["id"];
            usuario.Pessoa = PessoaData.getPessoaById((int)reader["id_pessoa"]);
            usuario.Senha = (string)reader["senha"];
            usuario.usuario = (string)reader["usuario"];
            usuario.DicaSenha = (string)reader["dica_senha"];
            usuario.Token = (string)reader["token"];

            return usuario;
        }
    }
}