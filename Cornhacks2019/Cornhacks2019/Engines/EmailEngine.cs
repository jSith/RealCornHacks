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
        private string _filePath = "../../../email/email.html";
        private string _body = "";

        IGithubAccessor _githubAccessor;

        public EmailEngine(IGithubAccessor githubAccessor)
        {
            _githubAccessor = githubAccessor;
        }

        public async void SendDigest(User user)
        {
            var repos = await _githubAccessor.GetPublicRepositoriesAsync();
            CreateEmail(repos);
            SendEmail(user);
        }

        public void CreateEmail(List<Repository> repos)
        {
            _body = File.ReadAllText(_filePath);

            for (int i = 0; i < 2; i++)
            {
                var repo = repos[i];
                var issue = repo.Issues.First();

                switch (i)
                {
                    case 0:
                        _body = _body.Replace("[RepoAvatar0]", repo.Owner.Avatar_Url);
                        _body = _body.Replace("[RepoTitle0]", repo.Name);
                        _body = _body.Replace("[RepoUrl0]", repo.Url);
                        _body = _body.Replace("[RepoDescription0]", repo.Description);
                        _body = _body.Replace("[RepoIssue0]", issue.Title);
                        break;

                    case 1:
                        _body = _body.Replace("[RepoAvatar1]", repo.Owner.Avatar_Url);
                        _body = _body.Replace("[RepoTitle1]", repo.Name);
                        _body = _body.Replace("[RepoUrl1]", repo.Url);
                        _body = _body.Replace("[RepoDescription1]", repo.Description);
                        _body = _body.Replace("[RepoIssue1]", issue.Title);
                        break;

                    case 2:
                        _body = _body.Replace("[RepoAvatar2]", repo.Owner.Avatar_Url);
                        _body = _body.Replace("[RepoTitle2]", repo.Name);
                        _body = _body.Replace("[RepoUrl2]", repo.Url);
                        _body = _body.Replace("[RepoDescription2]", repo.Description);
                        _body = _body.Replace("[RepoIssue2]", issue.Title);
                        break;

                    case 5:
                        _body = _body.Replace("[SponsRepoAvatar]", repo.Owner.Avatar_Url);
                        _body = _body.Replace("[SponsRepoTitle]", repo.Name);
                        _body = _body.Replace("[SponsRepoUrl]", repo.Url);
                        _body = _body.Replace("[SponsRepoDescription]", repo.Description);
                        _body = _body.Replace("[SponsRepoIssue]", issue.Title);
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