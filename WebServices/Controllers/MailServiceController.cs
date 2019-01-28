using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace WebServices.Controllers
{
    public class MailServiceController : ApiController
    {
        [HttpGet]
        public string SendEmail(string To, string Subject, string MailBody, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Email_ID, Email_ID);

            mail.To.Add(To);
            mail.Subject = Subject;

            mail.IsBodyHtml = isBodyHtml;
            mail.Body = MailBody;


            SmtpClient smtp = new SmtpClient(SMTPSERVER, Port);


            smtp.Credentials = new NetworkCredential(Email_ID, Email_Pasword);

            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                return "true";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }



        public int Port
        {
            get;

        } = 25;


        public string Email_ID
        {
            get;


        } = "e-auction@punjab.gov.pk";


        public string Email_Pasword
        {
            get;

        } = "P@kistan@786";


        public string SMTPSERVER
        {
            get;


        } = "mail.punjab.gov.pk";
    }
}
