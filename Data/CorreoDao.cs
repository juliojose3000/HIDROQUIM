using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace HIDROQUIM.Data
{
    public class CorreoDao
    {
        public CorreoDao()
        {
        }

        public void generarCorreo(string correo, string username, string contraseña, string nombreCliente) {
            MailMessage mail = new MailMessage();
            mail.To.Add(correo);
            mail.From = new MailAddress("keirm02@gmail.com");
            mail.Subject = "Correo Hidroquim";
            mail.Body = "Buenas "+nombreCliente+" sus datos para ingresar a la plataforma HIDROQUIM son: "+
                " USUARIO: "+ username + " CONTRASEÑA: "+ contraseña + "";


            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("keirm02@gmail.com", "keirm0203");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}