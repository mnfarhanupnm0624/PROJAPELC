using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace APELC.PublicShared
{
    public class EmailHelper
    {
        static string _encryptCode = System.Configuration.ConfigurationManager.AppSettings["encryptCode"];

        public static string SendEmail(string emailFrom, string emailTo, string emailSubject, string emailMessage)
        {
            string _return = "";
            try
            {
                var senderEmail = new MailAddress(emailFrom, "UTM-HR Admin");
                var receiverEmail = new MailAddress(emailTo, "Receiver");
                var password = "<YourGmailPassword>";
                var sub = emailSubject;
                var body = emailMessage;
                var smtp = new System.Net.Mail.SmtpClient
                {
                    //Host = "smtp.gmail.com",
                    Host = "smtp-relay.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                _return = "Success send eEmail to " + emailTo;
            }
            catch (Exception ex)
            {
                _return = "Error SendEmail TryCatch : " + ex;
            }
            return _return;
        }

        public static string GmailSendEmail(string emailFrom, string emailTo, string emailSubject, string emailMessage, string gmailAuthenticateEmail, string gmailAuthenticatePassword)
        {
            //var log = NLog.LogManager.GetCurrentClassLogger();
            //log.Info("GmailSendEmail emailFrom ~ " + emailFrom);
            //log.Info("GmailSendEmail emailTo ~ " + emailTo);
            //log.Info("GmailSendEmail emailSubject ~ " + emailSubject);
            //log.Info("GmailSendEmail emailMessage ~ " + emailMessage);
            //log.Info("GmailSendEmail gmailAuthenticateEmail ~ " + gmailAuthenticateEmail);
            //log.Info("GmailSendEmail gmailAuthenticatePassword ~ " + gmailAuthenticatePassword);

            string _authenticateEmail = EncryptHr.DecryptString4url(gmailAuthenticateEmail, "admSmis");
            string _authenticatePaswd = EncryptHr.DecryptString4url(gmailAuthenticatePassword, "admSmis");

            //log.Info("GmailSendEmail emailTo ~ " + emailTo);
            //log.Info("GmailSendEmail _authenticateEmail ~ " + _authenticateEmail);
            //log.Info("GmailSendEmail _authenticatePaswd ~ " + _authenticatePaswd);
            //emailTo = "zaman@utm.my";

            string _return = "";
            try
            {
                var authenticateSenderEmail = new MailAddress(_authenticateEmail, "Sender");
                var senderEmail = new MailAddress(_authenticateEmail, "UTMi-Visa Admin");
                var receiverEmail = new MailAddress(emailTo, "Receiver");
                var password = _authenticatePaswd;
                var sub = emailSubject;
                var body = emailMessage;

                var smtp = new SmtpClient
                {
                    Host = "smtp-relay.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(authenticateSenderEmail.Address, password)
                };
                var htmlBody = AlternateView.CreateAlternateViewFromString(emailMessage, Encoding.UTF8, "text/html");
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub
                })
                {
                    mess.AlternateViews.Add(htmlBody);
                    smtp.Send(mess);
                }
                _return = "Success send eEmail to " + emailTo;
            }
            catch (Exception ex)
            {
                _return = "Error GmailSendEmail TryCatch : " + ex;
            }
            return _return;
        }

        public static string GmailSendEmailFromLocal(string emailTo, string emailSubject, string emailMessage, string gmailAuthenticateEmail, string gmailAuthenticatePassword)
        {
            string _authenticateEmail = EncryptHr.DecryptString4url(gmailAuthenticateEmail, "admSmis");
            string _authenticatePaswd = EncryptHr.DecryptString4url(gmailAuthenticatePassword, "admSmis");

            string _return = "";
            try
            {
                var authenticateSenderEmail = new MailAddress(_authenticateEmail, "Sender");
                var senderEmail = new MailAddress(_authenticateEmail, "UTMi-Visa Admin");
                var receiverEmail = new MailAddress(emailTo, "Receiver");
                var password = _authenticatePaswd;
                var sub = emailSubject;
                var body = emailMessage;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(authenticateSenderEmail.Address, password)
                };
                var htmlBody = AlternateView.CreateAlternateViewFromString(emailMessage, Encoding.UTF8, "text/html");
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub
                })
                {
                    mess.AlternateViews.Add(htmlBody);
                    smtp.Send(mess);
                }
                _return = "Success send eEmail to " + emailTo;
            }
            catch (Exception ex)
            {
                _return = "Error GmailSendEmail TryCatch : " + ex;
            }
            return _return;
        }

        public static string GmailSendEmailWithTitle(string emailFrom, string emailTo, string emailSubject, string emailMessage, string gmailAuthenticateEmail, string gmailAuthenticatePassword, string emailFromTitle)
        {
            string _authenticateEmail = EncryptHr.DecryptString4url(gmailAuthenticateEmail, "admSmis");
            string _authenticatePaswd = EncryptHr.DecryptString4url(gmailAuthenticatePassword, "admSmis");

            //log.Info("GmailSendEmail emailTo ~ " + emailTo);
            //log.Info("GmailSendEmail _authenticateEmail ~ " + _authenticateEmail);
            //log.Info("GmailSendEmail _authenticatePaswd ~ " + _authenticatePaswd);

            string _return = "";
            try
            {
                var authenticateSenderEmail = new MailAddress(_authenticateEmail, "Sender");
                var senderEmail = new MailAddress(_authenticateEmail, (emailFromTitle != null ? emailFromTitle : "UTM-HR Admin"));
                var receiverEmail = new MailAddress(emailTo, "Receiver");
                var password = _authenticatePaswd;
                var sub = emailSubject;
                var body = emailMessage;

                var smtp = new SmtpClient
                {
                    //Host = "smtp.gmail.com",
                    Host = "smtp-relay.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(authenticateSenderEmail.Address, password)
                };
                var htmlBody = AlternateView.CreateAlternateViewFromString(emailMessage, Encoding.UTF8, "text/html");
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub
                })
                {
                    mess.AlternateViews.Add(htmlBody);
                    smtp.Send(mess);
                }
                _return = "Success send eEmail to " + emailTo;
            }
            catch (Exception ex)
            {
                _return = "Error GmailSendEmail TryCatch : " + ex;
            }
            return _return;
        }

        public static string GmailSendEmailByodWithTitle(string emailFrom, string emailTo, string emailSubject, string emailMessage, string gmailAuthenticateEmail, string gmailAuthenticatePassword, string emailFromTitle)
        {
            string _authenticateEmail = EncryptHr.DecryptString4url(gmailAuthenticateEmail, "admSmis");
            string _authenticatePaswd = EncryptHr.DecryptString4url(gmailAuthenticatePassword, "admSmis");

            string _return = "";
            try
            {
                var authenticateSenderEmail = new MailAddress(_authenticateEmail, "Sender");
                var senderEmail = new MailAddress(_authenticateEmail, (emailFromTitle != null ? emailFromTitle : "BYOD Admin"));
                var receiverEmail = new MailAddress(emailTo, "Receiver");
                var password = _authenticatePaswd;
                var sub = emailSubject;
                var body = emailMessage;

                SmtpClient smtp = (SmtpClient)MtdGeSmtpRelayGmailCom(authenticateSenderEmail, password);

                var htmlBody = AlternateView.CreateAlternateViewFromString(emailMessage, Encoding.UTF8, "text/html");
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub
                })
                {
                    mess.AlternateViews.Add(htmlBody);
                    smtp.Send(mess);
                }
                _return = "Success send eEmail to " + emailTo;
            }
            catch (Exception ex)
            {
                _return = "Error GmailSendEmail TryCatch : " + ex;
            }
            return _return;
        }

        private static object MtdGeSmtpRelayGmailCom(MailAddress authenticateSenderEmail, string password)
        {
            return new SmtpClient
            {
                //Host = "smtp.gmail.com",
                Host = "smtp-relay.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(authenticateSenderEmail.Address, password)
            };
        }

        public static bool IsValidEmail(string _textNya)
        {
            // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
            try
            {
                var addr = new System.Net.Mail.MailAddress(_textNya);
                return addr.Address == _textNya;
            }
            catch
            {
                return false;
            }
        }
    }
}
