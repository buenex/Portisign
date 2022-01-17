using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Portifolio_API.Models;

namespace Portifolio_API.Data
{
    public class Data{
        static string connectionString = "Server=localhost;Database=Portifolio;User Id=sa;Password=123456;";
        static public SqlConnection connection;
        static bool connect(){
            connection = new SqlConnection(connectionString);
            try{
                connection.Open();
                return true;
            }catch(Exception e){
                return false;
                throw e;
            }
        }

        static bool disconnect(){
            try{
                connection.Close();
                connection.Dispose();
                return true;
            }catch(Exception e){
                return false;
                throw e;
            }
        }

        public static bool executeNonQuery(string query,List<SqlParameter> parameters){
            SqlCommand command = new SqlCommand(query,connection);
            if(connect()){
                try{
                    command.Connection = connection;
                    command.CommandText = query;
                    addParameters(command,parameters);
                    command.ExecuteNonQuery();
                    disconnect();
                    return true;
                }catch(Exception e){
                    disconnect();
                    return false;
                    throw e;
                }
            }
            disconnect();
            return false;         
        }     

        public static T getObj<T>(string query,List<SqlParameter> parameters, Func<SqlDataReader, T> funcExtractObject)
                                                    where T : new()
        {
            T obj = new T();
            SqlCommand command = new SqlCommand(query,connection);
            SqlDataReader reader;        
            if(connect()){
                try{
                    command.Connection = connection;
                    command.CommandText = query;
                    addParameters(command,parameters);
                    reader = command.ExecuteReader();
                    while(reader.Read()){
                        obj = funcExtractObject(reader);
                    }                    
                }catch(Exception e){
                    disconnect();
                    throw e;
                }
            }
            disconnect();
            return obj;
        }   

        public static List<T> getObjs<T>(string query,List<SqlParameter> parameters,Func<SqlDataReader, T> funcExtractObject){
            List<T> objs = new List<T>();

            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            if(connect()){
                try{
                    command.Connection = connection;
                    command.CommandText = query;
                    addParameters(command,parameters);
                    reader = command.ExecuteReader();
                    while(reader.Read()){
                        objs.Add(funcExtractObject(reader));
                    }                    
                }catch(Exception e){
                    disconnect();
                    throw e;
                }
            }
            disconnect();
            return objs;
        }

        public static bool hasRows(string query,List<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(query,connection);
            SqlDataReader reader;        
            if(connect()){
                try{
                    command.Connection = connection;
                    command.CommandText = query;
                    addParameters(command,parameters);
                    reader = command.ExecuteReader();
                    if(reader.HasRows){
                        disconnect();
                        return true;
                    }else{
                        disconnect();
                        return false;
                    }               
                }catch(Exception e){
                    disconnect();
                    throw e;
                }
            }
            disconnect();
            return false;
        }

        public static void addParameters(SqlCommand command,List<SqlParameter> parameters){
            foreach(SqlParameter item in parameters){
                command.Parameters.Add(item);
            }
        }
    }
}