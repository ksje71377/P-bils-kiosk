using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// ⬇ nødvendige usings tilføjet til sidst:
using System;
using System.Windows.Threading;
using System.IO;
using P_bils_kiosk.Helpers;
using System.Net;
using System.Net.Mail;

namespace P_bils_kiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _dailyMailTimer;
        private bool _mailSentToday = false;

        public MainWindow()
        {
            InitializeComponent();

            _dailyMailTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _dailyMailTimer.Tick += DailyMailTimer_Tick;
            _dailyMailTimer.Start();
        }

        private void DailyMailTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            // Send gårsdagens fil kl. 00:05
            if (now.Hour == 00 && now.Minute == 05 && !_mailSentToday)
            {
                SendDailyMail(DateTime.Today.AddDays(-1));
                _mailSentToday = true;
            }

            // Reset flag kl. 00:10
            if (now.Hour == 0 && now.Minute == 10)
            {
                _mailSentToday = false;
            }
        }

//Skal på sigt flyttes ud af Main, dårlig kode.

        public void SendDailyMail(DateTime dato)
        {
            string datoStr = dato.ToString("dd-MM-yyyy");
            string fileName = $"{datoStr}.xlsx";

            // Hent sti til bin-mappen (samme sted som .exe kører fra)

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(basePath, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Excel-filen for {datoStr} blev ikke fundet og kunne ikke sendes.\nSti: {filePath}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var mailer = new AutoMailer();

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(mailer.SenderEmail);

                foreach (var modtager in mailer.MailingList)
                {
                    message.To.Add(modtager);
                }

                message.Subject = $"Dagens puljebils-log fra {datoStr}";
                message.Body = "Denne mail er automatisk genereret af Puljebils Software.Vedhæftet er bil-loggen for dagen.\n\nHusk at tilføje afsenderadressen som godkendt, for at forhindre at den ryger i junkfilteret.\n\nHar du behov for at tilføje eller fjerne e-mail adresser? Log ind i administratorpanelet på computeren.";
                message.Attachments.Add(new Attachment(filePath));
                message.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient(mailer.SmtpHost)
                {
                    Port = mailer.SmtpPort,
                    Credentials = new NetworkCredential(mailer.SenderEmail, mailer.SenderPassword),
                    EnableSsl = true
                };

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved afsendelse af email: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            //Static metode, så manuel mail kan sendes fra Admin klassen.

            public static void SendDailyMailStatic(DateTime dato)
            {
            // Find aktiv instans af MainWindow
            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow mainWindow)
                {
                    mainWindow.SendDailyMail(dato);
                    return;
                }
            }

            MessageBox.Show("Kunne ikke finde MainWindow-instansen.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
