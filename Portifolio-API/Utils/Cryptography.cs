using System;

namespace Portifolio_API.Utils{
    public static class Cryptography{
        static string getCharAleatorio(){
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!*()-+}{][|";
            Random rnd = new Random();
            return chars.Substring(rnd.Next(0,chars.Length),1);
        }

        static string cryptography(string str){
            int length = str.Length;
            string temp = "";

            for(int i=0; i<length; i++){
                temp += getCharAleatorio();
                temp += str.Substring(i,1).ToString();
            }

            return temp;
        }

        static string decryptography(string str){
            int length = str.Length;
            string temp = "";

            for(int i=0; i<length; i++){
                if(i%2 != 0){
                    temp += str.Substring(i,1);
                }
            }

            return temp;
        }

        public static string cryptographyLoop(string str){
            string temp = str;

            for(int i=0;i<5;i++){
                temp = cryptography(temp);
            }

            return temp;
        }

        public static string decryptographyLoop(string str){
            string temp = str;

            for(int i=0;i<5;i++){
                temp = decryptography(temp);
            }

            return temp;
        }
    }
}