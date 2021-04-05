using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class Email
    {
        private readonly IConfiguration config;
        public Email(IConfiguration config)
        {
            this.config = config;
        }

        public void EmailService(forgetclass link)
        {
            try
            {
                string HtmlBody;
                using (StreamReader streamReader = new StreamReader(config["IssuerEmailDetail:HtmlBodyFile"], Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                HtmlBody = HtmlBody.Replace("JwtToken", link.JwtToken);
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(config["IssuerEmailDetail:Email"]);
                message.To.Add(new MailAddress(link.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(config["IssuerEmailDetail:Email"], config["IssuerEmailDetail:Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
