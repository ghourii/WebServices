using System.Text;
using System.Net.Mail;
using System.IO;
using System;
using System.Net;
using System.Configuration;
using System.Web;

namespace WebServices.Utills
{
    public class MyEmail
    {
        public bool SendEmail(string toEmail, string subject, string body,String display_name="",bool IsHtml=true)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(GetValue("SmtpEmail"),display_name);
            
            mail.To.Add(toEmail);
            mail.Subject = subject;

            mail.IsBodyHtml = IsHtml;
            mail.Body = body;
            

            SmtpClient smtp = new SmtpClient(GetValue("Smtp"), Numerics.GetInt(GetValue("Port")));
         

            smtp.Credentials = new NetworkCredential(GetValue("SmtpEmail"), GetValue("SmtpPassword"));

            smtp.EnableSsl = Convert.ToBoolean(GetValue("EnableSsl"));
           
            try
            {
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

          
        }

        public bool SendEmail(string from,string port,string host,string password,string toEmail, string subject, string body, String to_name_of_user, String display_name = "", bool IsHtml = true, bool IsSSL = false)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, display_name);

            mail.To.Add(toEmail);
            mail.Subject = subject;

            mail.IsBodyHtml = IsHtml;
            mail.Body = body;


            SmtpClient smtp = new SmtpClient(host, Numerics.GetInt(port));


            smtp.Credentials = new NetworkCredential(from, password);

            smtp.EnableSsl = IsSSL;

            try
            {
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }


        
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }


    }
}
