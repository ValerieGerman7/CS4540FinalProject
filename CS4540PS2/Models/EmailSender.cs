using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CS4540PS2.Services {
    public class EmailSender : IEmailSender {

        public Task SendEmailAsync(string email, string subject, string message) {
            var sendTask = new Task(() => {
                SmtpClient client = new SmtpClient("smtp.utah.edu", 25);
                client.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("admin@cs4540.com", "LearningOutcomes");
                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                //client.Send(mail);
            });
            sendTask.Start();
            return sendTask;
        }
    }
}
