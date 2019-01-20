using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cornhacks2019.Engines
{
    public class EmailEngine
    {
        public void CreateEmail(Dictionary<Repository, List<Issue>> dictionary)
        {

        }

        
        public void SendEmail(User user)
        {            
            var basicCredential = new NetworkCredential("cornhacksteamtwo@gmail.com", "testingpass1");
            MailMessage mail = new MailMessage("cornhacksteamtwo@gmail.com", user.Email);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential;
            client.Host = "smtp.gmail.com";

            mail.IsBodyHtml = true;
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }

        public string GetTarget(User user)
        {
            return user.Email;
        }
    }
}
