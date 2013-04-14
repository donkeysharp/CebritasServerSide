using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cebritas.General.Networking {
    public class EmailUtil {
        public string MailServer { get; set; }
        public int Port { get; set; }
        public bool IsHtml { get; set; }

        private string senderEmail;
        private string senderPassword;
        private MailMessage mailMessage;

        public EmailUtil(string mailServer, int port, string senderEmail, string senderPassword) {
            this.MailServer = mailServer;
            this.Port = port;
            this.IsHtml = false;

            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
            this.mailMessage = new MailMessage();

            this.mailMessage.From = new MailAddress(senderEmail);
            this.mailMessage.BodyEncoding = Encoding.UTF8;
            this.mailMessage.SubjectEncoding = Encoding.UTF8;

            SetCredentials(senderEmail, senderPassword);
        }

        public void SetCredentials(string senderEmail, string senderPassword) {
            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
        }

        public void AddRecipient(string recipientEmail) {
            this.mailMessage.To.Add(recipientEmail);
        }

        public void AddRecipient(List<string> recipients) {
            foreach (string recipient in recipients) {
                AddRecipient(recipients);
            }
        }

        public void SetBody(string message) {
            this.mailMessage.Body = message;
        }

        public void SendMessage(string subject) {
            this.mailMessage.Subject = subject;

            SmtpClient client = new SmtpClient();
            client.Host = this.MailServer;
            client.Port = this.Port;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(this.senderEmail, this.senderPassword);
            client.EnableSsl = true;
            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            try {
                //client.Send(this.mailMessage);
                client.Send(this.mailMessage);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            if (e.Error != null) {
                throw new CebraException(e.Error.Message);
            }
        }
    }
}