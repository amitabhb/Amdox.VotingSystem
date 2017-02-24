//-----------------------------------------------------------------------
// <copyright file="EmailService.cs" company="amitabh barua">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Amdox.Common
{
    public static class EmailService
    {
        /// <summary>
        /// This will send the email.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="from"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendEmail(List<string> recipients, string from, List<string> cc, string subject, string msgBody, List<string> bcc)
        {
            bool success = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.IsBodyHtml = true;

            if (recipients != null && recipients.Count > 0)
            {
                foreach (string recp in recipients)
                {
                    message.To.Add(new MailAddress(recp));
                }
            }
            if (cc!=null && cc.Count > 0)
            {
                foreach (string recp in cc)
                {
                    message.CC.Add(new MailAddress(recp));
                }
            }
            if (bcc!=null && bcc.Count > 0)
            {
                foreach (string recp in bcc)
                {
                    message.Bcc.Add(new MailAddress(recp));
                }
            }

            //if(!string.IsNullOrEmpty(cc))
            //{
            //    message.To.Add(new MailAddress(cc));
            //}

            message.Subject = subject;
            message.Body = msgBody;
            message.Headers.Add("Sensitivity", "Private");
            SmtpClient client = new SmtpClient();
            client.Send(message);
            success = true;
            
            return success;
        }
    }
}
