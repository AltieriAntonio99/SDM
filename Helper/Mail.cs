using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using SDM.Models;
using SDM.Models.Database;

namespace SDM.Helper
{
    public class Mail
    {
        public void SendMail(Pratica pratica, string type, string to, string subject)
        {
            using (var context = new SDMEntities())
            {
                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.Subject = subject + " - " + pratica.NumPratica;
                message.IsBodyHtml = true;
                string host = HttpContext.Current.Request.Url.Host;
                string url = "https://" + host + "/Home/RiepilogoPratica?id=" + pratica.Id + "&type=" + type;
                if (host == "localhost")
                {
                    url = "https://localhost:44323/Home/RiepilogoPratica?id=" + pratica.Id + "&type=" + type;
                }

                var user = context.Users.SingleOrDefault(i => i.Id == pratica.IdUserUpdate);
                var sede = user.Sedi.Nome;

                message.Body = @"<html>
                                <head>
                                </head>
                                <body>
                                    <div>
                                        <div style='width: 350px;
                                            padding: 10px;
                                            margin: 20px auto;
                                            text-align: center;
                                            height: 350px;
                                            border-radius: 17px;
                                            border: 3px solid #010045;
                                            box-shadow: 0px 0px 12px 6px #939393;
                                            background-color: #ffffff;'>
                                            <h1 style='text-align: center; 
                                                font-size: 33px;'>Pratica</h1>
                                            <p style='text-align: center; 
                                                margin: 40px 0px; 
                                                font-size: 18px;'>
                                                Numero pratica <span style='font-weight: bold'>" + pratica.NumPratica + @"</span> 
                                                inserita da <span style='font-weight: bold'>" + user.Username + @"</span>
                                                sede di <span style='font-weight: bold'>" + sede + @"</span>
                                                il <span style='font-weight: bold'>" + pratica.LastUpdate + @"</span>
                                            </p>
                                            <a style='padding: 10px; 
                                                background-color: #010045; 
                                                border: none; 
                                                color: white;
                                                text-decoration: none;' href=" + url + @">
                                                Vai alla pratica
                                            </a>
                                        </div>
                                    </div>
                                </body>
                                </html>
                                ";

                message.IsBodyHtml = true;

                message.From = new MailAddress("portalesdm@gmail.com");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("portalesdm@gmail.com", "ydtrqblxlkrydqot");
                smtp.Send(message);
            }            
        }

        public void SendMail(Pratica pratica, string type, string to, List<string> cc, string subject)
        {
            using (var context = new SDMEntities())
            {
                MailMessage message = new MailMessage();
                message.To.Add(to);

                foreach (var item in cc)
                {
                    message.CC.Add(item);
                }

                message.Subject = subject + " - " + pratica.NumPratica;
                message.IsBodyHtml = true;
                string host = HttpContext.Current.Request.Url.Host;
                string url = "https://" + host + "/Home/RiepilogoPratica?id=" + pratica.Id + "&type=" + type;
                if (host == "localhost")
                {
                    url = "https://localhost:44323/Home/RiepilogoPratica?id=" + pratica.Id + "&type=" + type;
                }

                var user = context.Users.SingleOrDefault(i => i.Id == pratica.IdUserUpdate);
                var sede = user.Sedi.Nome;

                message.Body = @"<html>
                                <head>
                                </head>
                                <body>
                                    <div>
                                        <div style='width: 350px;
                                            padding: 10px;
                                            margin: 20px auto;
                                            text-align: center;
                                            height: 350px;
                                            border-radius: 17px;
                                            border: 3px solid #010045;
                                            box-shadow: 0px 0px 12px 6px #939393;
                                            background-color: #ffffff;'>
                                            <h1 style='text-align: center; 
                                                font-size: 33px;'>Pratica</h1>
                                            <p style='text-align: center; 
                                                margin: 40px 0px; 
                                                font-size: 18px;'>
                                                Numero pratica <span style='font-weight: bold'>" + pratica.NumPratica + @"</span> 
                                                inserita da <span style='font-weight: bold'>" + user.Username + @"</span>
                                                sede di <span style='font-weight: bold'>" + sede + @"</span>
                                                il <span style='font-weight: bold'>" + pratica.LastUpdate + @"</span>
                                            </p>
                                            <a style='padding: 10px; 
                                                background-color: #010045; 
                                                border: none; 
                                                color: white;
                                                text-decoration: none;' href=" + url + @">
                                                Vai alla pratica
                                            </a>
                                        </div>
                                    </div>
                                </body>
                                </html>
                                ";

                message.IsBodyHtml = true;

                message.From = new MailAddress("portalesdm@gmail.com");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("portalesdm@gmail.com", "bswweyzrtmdyauqz");
                smtp.Send(message);
            }
        }

        public void SendMail(List<string> tos)
        {
            using (var context = new SDMEntities())
            {
                MailMessage message = new MailMessage();
                foreach (var to in tos)
                {
                    message.To.Add(to);
                }
                message.Subject = "Nuovi documenti nella sezione news";
                message.IsBodyHtml = true;
                string host = HttpContext.Current.Request.Url.Host;
                string url = "https://" + host + '"';
                if (host == "localhost")
                {
                    url = "https://localhost:44388/";
                }

                message.Body = @"<html>
                                <head>
                                </head>
                                <body>
                                    <div>
                                        <div style='width: 350px;
                                            padding: 10px;
                                            margin: 20px auto;
                                            text-align: center;
                                            height: 350px;
                                            border-radius: 17px;
                                            border: 3px solid #010045;
                                            box-shadow: 0px 0px 12px 6px #939393;
                                            background-color: #ffffff;'>
                                            <h1 style='text-align: center; 
                                                font-size: 33px;'>Ci sono nuovi documenti nella sezione News</h1>
                                            <p style='text-align: center; 
                                                margin: 40px 0px; 
                                                font-size: 18px;'>
                                                Clicca il pulsante per accedere al sito
                                            </p>
                                            <a style='padding: 10px; 
                                                background-color: #010045; 
                                                border: none; 
                                                color: white;
                                                text-decoration: none;' href=" + url + @">
                                                Accedi al sito
                                            </a>
                                        </div>
                                    </div>
                                </body>
                                </html>
                                ";

                message.IsBodyHtml = true;

                message.From = new MailAddress("portalesdm@gmail.com");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("portalesdm@gmail.com", "bswweyzrtmdyauqz");
                smtp.Send(message);
            }
        }

    }
}