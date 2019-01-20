using Cornhacks2019.Models;
﻿using CornHacks2019.Interfaces.EngineInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cornhacks2019.Engines
{
    public class EmailEngine : IEmailEngine
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

            string filePath = "../../../../email/email.html";
            string body = File.ReadAllText(filePath);
            mail.IsBodyHtml = true;
            mail.Subject = "CodeCrowd Newsletter";
            mail.Body = body;
            client.Send(mail);
        }

        public string GetTarget(User user)
        {
            return user.Email;
        }
    }
}
