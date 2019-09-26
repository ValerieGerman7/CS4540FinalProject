using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains an email sender, which sends a given email from the admin@cs4540.com LearningOutcomes.
/// </summary>
namespace CS4540PS2.Services {
    public class EmailSender : IEmailSender {

        /// <summary>
        /// Sends an email from admin@cs4540.com LearningOutcomes to the given destination. This server must
        /// be run on the UofU network for this to be functional.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
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

                client.Send(mail);
            });
            sendTask.Start();
            return sendTask;
        }
    }
}
