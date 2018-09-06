﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Text;

namespace SurveyApp
{
    public class SMTPHelper
    {
        public static bool SendEmail(string body, string subject, IEnumerable<string> TOs, bool isHtml, IEnumerable<string> CCs = null, IEnumerable<string> BCCs = null)
        {
            string smtpServer = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPSERVER"]); //"smtp.gmail.com";
            int smtpPort = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPPORT"]);
            string sender = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPSENDER"]);
            string password = Encryption.Decrypt(Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["key2"]), System.Web.Configuration.WebConfigurationManager.AppSettings["key5"].ToString(), false);

            var message = new MailMessage { Body = body, BodyEncoding = Encoding.UTF8, IsBodyHtml = isHtml, Subject = subject, From = new MailAddress(sender) };

            foreach (var r in TOs)
            {
                message.To.Add(r);
            }

            if (CCs != null)
            {
                foreach (var c in CCs)
                {
                    message.CC.Add(c);
                }
            }

            if (BCCs != null)
            {
                foreach (var c in BCCs)
                {
                    message.Bcc.Add(c);
                }
            }

            using (SmtpClient objSMTPClient = new SmtpClient())
            {
                objSMTPClient.EnableSsl = true;
                objSMTPClient.UseDefaultCredentials = false;
                objSMTPClient.Credentials = new NetworkCredential(sender, password);
                objSMTPClient.Host = smtpServer;
                objSMTPClient.Port = smtpPort;
                objSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    objSMTPClient.Send(message);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool SendGridEmail(string subject, string body, IEnumerable<string> TOs, bool isHtml, IEnumerable<string> CCs = null, IEnumerable<string> BCCs = null)
        {
            string sender = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPSENDER"]);
            string password = Encryption.Decrypt(Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["key2"]), System.Web.Configuration.WebConfigurationManager.AppSettings["key5"].ToString(), false);

            try
            {
                var msg = new SendGridMessage();                
                msg.Subject = subject;
                msg.Html = body;

                msg.From = new MailAddress("no-reply@ucsfebit.azurewebsites.net");

                bool isEmailEnabled = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmails"]);
                if (isEmailEnabled)
                {
                    msg.AddTo(TOs);
                    List<MailAddress> ccEmails = new List<MailAddress>();
                    List<MailAddress> bccEmails = new List<MailAddress>();

                    if (CCs != null)
                    {
                        foreach (var c in CCs)
                        {
                            ccEmails.Add(new MailAddress(c));
                        }
                        msg.Cc = ccEmails.ToArray();
                    }

                    if (BCCs != null)
                    {
                        foreach (var c in BCCs)
                        {
                            bccEmails.Add(new MailAddress(c));
                        }                        
                    }

                    bccEmails.Add(new MailAddress("ebitdevteam@gmail.com"));
                    msg.Bcc = bccEmails.ToArray();

                }
                else
                {
                    msg.AddTo(System.Web.Configuration.WebConfigurationManager.AppSettings["TestEmailAddress"].ToString());
                }

                var credentials = new NetworkCredential(sender, password);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(msg);
                

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}