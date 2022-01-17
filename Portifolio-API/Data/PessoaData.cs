using System;
using System.Data.SqlClient;
using Portifolio_API.Models;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Portifolio_API.Data{
    public class PessoaData:Data{
        public static List<Pessoa> getAllPessoa(){
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM Pessoa ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            return getObjs<Pessoa>(query.ToString(),parameters,extractObject);
        }

        public static Pessoa getPessoaById(int id){
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM Pessoa ");
            query.Append("WHERE id = @id");

            SqlParameter parameter = new SqlParameter("@id",SqlDbType.Int);
            parameter.Value = id;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(parameter);

            return getObj<Pessoa>(query.ToString(),parameters,extractObject);
        }

        public static Pessoa getPessoaByCpf(string cpf){
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * FROM Pessoa ");
            query.Append("WHERE cpf = @cpf");

            SqlParameter parameter = new SqlParameter("@cpf",SqlDbType.VarChar,14);
            parameter.Value = cpf;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(parameter);

            return getObj<Pessoa>(query.ToString(),parameters,extractObject);
        }

        public static bool insertPessoa(Pessoa pessoa){
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO Pessoa ");
            query.Append("VALUES(@nome,@cpf,@email)");

            SqlParameter param_nome = new SqlParameter("@nome",SqlDbType.VarChar,200);
            SqlParameter param_cpf = new SqlParameter("@cpf",SqlDbType.VarChar,14);
            SqlParameter param_email = new SqlParameter("@email",SqlDbType.VarChar,200);

            param_nome.Value = pessoa.Nome;
            param_cpf.Value = pessoa.Cpf;
            param_email.Value = pessoa.Email;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(param_nome);
            parameters.Add(param_cpf);
            parameters.Add(param_email);

            return executeNonQuery(query.ToString(),parameters);
        }

        public static Pessoa insertAndGetIdPessoa(Pessoa pessoa){
            if(insertPessoa(pessoa)){
                return getPessoaByCpf(pessoa.Cpf);
            }
            return null;
        }

        public static Pessoa extractObject(SqlDataReader reader){
            Pessoa pessoa = new Pessoa();

            pessoa.Id = (int)reader["id"];
            pessoa.Nome = (string)reader["nome"];
            pessoa.Cpf = (string)reader["cpf"];
            pessoa.Email = (string)reader["email"];

            return pessoa;
        }
    }
}