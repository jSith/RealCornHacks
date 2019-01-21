using Cornhacks2019.Models;
using Cornhacks2019.Interfaces.AccessorInterfaces;
using Cornhacks2019.Interfaces.EngineInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cornhacks2019.Engines
{
    public class EmailEngine : IEmailEngine
    {
        private string _filePath = "../../../../email/email.html";
        private string _body = "";

        private IGithubAccessor _githubAccessor;

        public async void SendDigest(User user)
        {
            var repos = await _githubAccessor.GetPublicRepositoriesAsync();
            CreateEmail(repos);
            SendEmail(user);
        }

        public void CreateEmail(List<Repository> repos)
        {
            _body = File.ReadAllText(_filePath);

            for (int i = 0; i < 4; i++)
            {
                var repo = repos[i];
                var issue = repo.Issues.First();

                _body = _body.Replace($"[RepoAvatar{i}]", repo.Owner.Avatar_Url);
                _body = _body.Replace($"[RepoTitle{i}]", repo.Name);
                _body = _body.Replace($"[RepoUrl{i}]", repo.Url);
                _body = _body.Replace($"[RepoDescription{i}]", repo.Description);
                _body = _body.Replace($"[RepoIssue{i}]", issue.Title);
               
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