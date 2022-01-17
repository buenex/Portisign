using System;
using System.Net.Mail;
using System.Net; 
using static Portifolio_API.Utils.MailFactory;

namespace Portifolio_API.Utils{
    public static class Mail{
        public static void sendMessage(string to,string subject,string body,bool isBodyHtml=false){
            try
            {
                SmtpClient client = GetMailSmtpClient(Configuration.host);
                
                client.Send(GetMailMessage(Configuration.email,to,subject,body,isBodyHtml));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }
    }
}