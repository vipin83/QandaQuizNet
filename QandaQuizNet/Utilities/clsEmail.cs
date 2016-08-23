using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QandaQuizNet.Utilities
{
    public class clsEmail
    {
        public Task SendEmailUsingSMTP(string emailTo, string emailFrom, string subject, string emailBody)
        {

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(emailFrom);
            mailMsg.To.Add(emailTo);
            mailMsg.Subject = subject;
            mailMsg.Body = emailBody;

            mailMsg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("localhost", 25);
            smtp.Credentials = new System.Net.NetworkCredential("admin@QandaQuiz.com", "Pa$$word1");

            try
            {
                return smtp.SendMailAsync(mailMsg);
            }
            catch (SmtpException ex)
            { }

           return Task.FromResult(0);
        }

    }
}