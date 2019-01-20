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
        private string _filePath = "../../../../email/email.html";
        private string _body = "";

        public void CreateEmail(Dictionary<int, KeyValuePair<Repository, Issue>> dictionary)
        {
            _body = File.ReadAllText(_filePath);
            
            for (int i = 0; i < 5; i++)
            {
                var pair = dictionary.GetValueOrDefault(i);
                switch (i)
                {                    
                    case 0:
                        _body.Replace("[RepoAvatar0]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[RepoTitle0]", pair.Key.Name);
                        _body.Replace("[RepoUrl0]", pair.Key.Url);
                        _body.Replace("[RepoDescription0]", pair.Key.Description);
                        _body.Replace("[RepoIssue0]", pair.Value.Title);
                        break;

                    case 1:
                        _body.Replace("[RepoAvatar1]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[RepoTitle1]", pair.Key.Name);
                        _body.Replace("[RepoUrl1]", pair.Key.Url);
                        _body.Replace("[RepoDescription1]", pair.Key.Description);
                        _body.Replace("[RepoIssue1]", pair.Value.Title);
                        break;

                    case 2:
                        _body.Replace("[RepoAvatar2]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[RepoTitle2]", pair.Key.Name);
                        _body.Replace("[RepoUrl2]", pair.Key.Url);
                        _body.Replace("[RepoDescription2]", pair.Key.Description);
                        _body.Replace("[RepoIssue2]", pair.Value.Title);
                        break;

                    case 3:
                        _body.Replace("[RepoAvatar3]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[RepoTitle3]", pair.Key.Name);
                        _body.Replace("[RepoUrl3]", pair.Key.Url);
                        _body.Replace("[RepoDescription3]", pair.Key.Description);
                        _body.Replace("[RepoIssue3]", pair.Value.Title);
                        break;

                    case 4:
                        _body.Replace("[RepoAvatar4]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[RepoTitle4]", pair.Key.Name);
                        _body.Replace("[RepoUrl4]", pair.Key.Url);
                        _body.Replace("[RepoDescription4]", pair.Key.Description);
                        _body.Replace("[RepoIssue4]", pair.Value.Title);
                        break;

                    case 5:
                        _body.Replace("[SponsRepoAvatar]", pair.Key.Owner.Avatar_Url);
                        _body.Replace("[SponsRepoTitle]", pair.Key.Name);
                        _body.Replace("[SponsRepoUrl]", pair.Key.Url);
                        _body.Replace("[SponsRepoDescription]", pair.Key.Description);
                        _body.Replace("[SponsRepoIssue]", pair.Value.Title);
                        break;
                }
            }
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

            mail.Subject = "CodeCrowd Newsletter";
            mail.Body = _body;

            client.Send(mail);
        }

        public string GetTarget(User user)
        {
            return user.Email;
        }
    }
}
