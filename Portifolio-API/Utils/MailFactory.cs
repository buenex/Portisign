using System;
using System.Net.Mail;
using System.Net;

namespace Portifolio_API.Utils{
    public static class MailFactory{
        public static MailMessage GetMailMessage(string from, string to,string subject,string body, bool isBodyHtml = false){
            MailMessage message = new MailMessage(from,to,subject,body);
            message.IsBodyHtml = isBodyHtml;

            return message;
        }
        public static SmtpClient GetMailSmtpClient(int emailType){
            switch(emailType){
                case (int)Enums.EmailType.outlook:
                    return new SmtpClient{
                        Port = 587, 
                        Credentials = new NetworkCredential(Configuration.email,Configuration.senha),
                        EnableSsl = true,
                        Host = "smtp.office365.com"
                    };
                default:
                    return null;
            }
        }
    }
}