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
        private string SmtpHost { get; set; }
        private int SmtpPort { get; set; }
        private string SenderEmail { get; set; }
        private string SenderPassword { get; set; }
        private List<string> MailingList { get; set; }

        public AutoMailer()
        {
            // Læs konfiguration fra appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
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