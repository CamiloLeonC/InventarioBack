using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace General_back.Helpers
{
    public class EnviarCorreoHelper
    {
        public static void enviarCorreo(IConfiguration config, string asuntoCorreo, string cuerpoCorreo, string correo)
        {
            try
            {
                SmtpClient client = new SmtpClient(config["SmtpClient"]);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(config["email"], config["emailPass"]);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(config["email"]);
                mailMessage.To.Add(correo);
                mailMessage.Subject = asuntoCorreo;
                mailMessage.Body = cuerpoCorreo;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }
    }
}
