using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace WcfService1.Outil
{
    public class GestionMail
    {
        private MailMessage monMail;
        private DictionnaireReponseUpdateBase drub;
        private string cle;
        private string pseudo;
        private string mail_no_reply;
        private string host;
        private string identifiant_mail;
        private string mdp_mail;
        private string port;
        private string corps_mail;

        public GestionMail(string cle, string pseudo, string mail_destinataire)
        {
            this.cle = cle;
            this.pseudo = pseudo;
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                mail_no_reply = ConfigurationManager.AppSettings["mail_no_reply"];
            }
            catch (FormatException e)
            {
                mail_no_reply = "";
            }

            try
            {
                host = ConfigurationManager.AppSettings["host_mail"];
            }
            catch (FormatException e)
            {
                host = "";
            }

            try
            {
                port = ConfigurationManager.AppSettings["port"];
            }
            catch (FormatException e)
            {
                port = "";
            }

            try
            {
                identifiant_mail = ConfigurationManager.AppSettings["identifiant_mail"];
            }
            catch (FormatException e)
            {
                identifiant_mail = "";
            }

            try
            {
                mdp_mail = ConfigurationManager.AppSettings["mdp_mail"];
            }
            catch (FormatException e)
            {
                mdp_mail = "";
            }

            try
            {
                corps_mail = ConfigurationManager.AppSettings["corps_mail"];
            }
            catch (FormatException e)
            {
                corps_mail = "";
            }

            monMail = new MailMessage();
            monMail.From = new MailAddress(mail_no_reply);

            monMail.To.Add(new MailAddress(mail_destinataire));

            monMail.Subject = "Demande de réinitialisation du mot de passe";
            monMail.IsBodyHtml = true;
            monMail.Body = generationBodyMail();
        }

        private string generationBodyMail()
        {
            string body = "";
            if (!corps_mail.Equals(""))
            {
                string texteHTML = System.IO.File.ReadAllText(corps_mail, Encoding.UTF8);
                body = texteHTML.Replace("#CLE#", cle).Replace("#PSEUDO#", pseudo);
            }
            return body;
        }

        public ReponseUpdateBase envoyerMail()
        {
            if (monMail != null && !cle.Equals("") && !pseudo.Equals("") && !mail_no_reply.Equals("") && !host.Equals("") && !identifiant_mail.Equals("") && !mdp_mail.Equals("") && !port.Equals(""))
            {
                SmtpClient client = new SmtpClient();

                // définition du serveur smtp
                client.Host = host;
                client.Port = Convert.ToInt32(port);
                client.EnableSsl = true;

                // définition des login et pwd si smtp sécurisé
                client.Credentials = new NetworkCredential(identifiant_mail, mdp_mail);

                client.Send(monMail);
                return drub.getReponseUpdateBase(27);
            }
            else
            {
                return drub.getReponseUpdateBase(26); ;
            }
        }
    }
}