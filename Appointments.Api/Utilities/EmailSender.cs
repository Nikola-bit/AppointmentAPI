using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AppointmentsDTO.DTO;
using Appointments.Api.Utilities;

namespace Appointments.API.Utilities
{
    public class EmailSender
    {
        public async static Task<bool> SendEmail(MailDTO information)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("prendjov.portfolio@gmail.com", "Appointment Management Agency");
            message.To.Add(new MailAddress(information.RecieverEmail));
            message.Subject = information.Subject;
            message.IsBodyHtml = false;
            message.Body = information.Body;
           
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Timeout = 60000;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("prendjov.portfolio@gmail.com", DataEncryption.Decrypt("SgKi+XCRnhq7XvKfPgM8xqkde+6q6a9DmhgLmwK2lks="));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message);
            message.Dispose();

            return true; //new
        }
    }
}