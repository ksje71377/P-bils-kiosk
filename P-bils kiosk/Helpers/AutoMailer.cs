using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace P_bils_kiosk.Helpers
{
    public class AutoMailer
    {
        public string SmtpHost { get; private set; }
        public int SmtpPort { get; private set; }
        public string SenderEmail { get; private set; }
        public string SenderPassword { get; private set; }
        public List<string> MailingList { get; private set; }

        public AutoMailer()
        {
            // Læs konfiguration fra MailingList.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(Path.Combine("databases", "MailingList.json"))
                .Build();

            var smtpConfig = config.GetSection("Smtp");
            SmtpHost = smtpConfig["Host"];
            SmtpPort = int.Parse(smtpConfig["Port"]);
            SenderEmail = smtpConfig["SenderEmail"];
            SenderPassword = smtpConfig["SenderPassword"];

            MailingList = config.GetSection("MailingList").Get<List<string>>();
        }

        public void SendEmail(string subject, string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(SenderEmail);

            foreach (var recipient in MailingList)
            {
                mail.To.Add(recipient);
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            var smtpClient = new SmtpClient(SmtpHost)
            {
                Port = SmtpPort,
                Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sendt!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved afsendelse: " + ex.Message);
            }
        }
    }
}