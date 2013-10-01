using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZAOSCore;
using CZBizObjects;
using CZDataObjects;

namespace CZAOSMail.Mail
{
    using System.Net.Mail;
    using System.Net;
    using System.Web;
    using KT.Extensions;
    using System.Net.Mime;

    public abstract class SiteEmailBase
    {
        protected string mstrLastError = string.Empty;
        protected string mstrLastErrorTitle = string.Empty;
        public string LastError
        {
            get { return mstrLastError; }
        }

        public string LastErrorTitle
        {
            get { return mstrLastErrorTitle; }
        }

        protected static MailAddress AdminReplyAddress
        {
            get { return new MailAddress(AppSettings.GetSetting("AdminReplyAddress"), AppSettings.GetSetting("EntityTitle")); }
        }

        protected string GetBaseUrl()
        {
            return HttpContext.Current.GetSiteUrl();           

        }

        protected void SendMail(EmailMessage message, string htmlBody)
        {
            mstrLastError = string.Empty;
            mstrLastErrorTitle = string.Empty;

            EmailTracking tracker = new EmailTracking();

            tracker.To = message.To.ToString();
            tracker.From = message.From.ToString();
            tracker.Subject = message.Subject;
            tracker.Body = htmlBody;

            tracker = EmailTrackingList.AddItem(tracker);

            htmlBody = tracker.AppendToken(htmlBody);

            if (!Convert.ToBoolean(AppSettings.GetSetting("NotificationsOn")))
            {
                mstrLastError = "Email Notifications are turned off";
                mstrLastErrorTitle = mstrLastError;

                tracker.Sent = false;
                tracker.FailReason = mstrLastError;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);

                return;
            }


            if (htmlBody.IsNotNullOrEmpty())
            {
                System.Net.Mime.ContentType htmlContentType = new System.Net.Mime.ContentType("text/html");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(htmlBody, htmlContentType);
                message.AlternateViews.Add(htmlView);
                message.IsBodyHtml = true;

            }
            else
            {
                mstrLastError = "Email body is empty";
                mstrLastErrorTitle = mstrLastError;

                tracker.Sent = false;
                tracker.FailReason = mstrLastError;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);

                return;
            }
            

            if (message.From.Address.IsNullOrEmpty())
            {
                message.From = AdminReplyAddress;
            }            

            try
            {
                message.RemoveDuplicateAddresses();

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.EnableSsl = message.EnableSsl;

                if (message.SMTPServer.IsNotNullOrEmpty())
                {
                    smtp.Host = message.SMTPServer;


                    if (message.SMTPServerLogin.IsNotNullOrEmpty())
                    {
                        if (message.SMTPServerPassword.IsNotNullOrEmpty())
                        {
                            smtp.Credentials = new NetworkCredential(message.SMTPServerLogin, message.SMTPServerPassword);
                        }

                    }

                    if (message.SMTPPort > 0)
                    {
                        smtp.Port = message.SMTPPort;
                    }

                }

                smtp.Send(message);
                mstrLastError = string.Empty;
                mstrLastErrorTitle = mstrLastError;

                tracker.Sent = true;
                tracker.FailReason = string.Empty;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);
           

            }
            catch (Exception ex)
            {
                mstrLastError = ex.GetCompleteErrorMessage();
                mstrLastErrorTitle = ex.Message;

                tracker.Sent = false;
                tracker.FailReason = mstrLastError;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);
            }

        }

        protected void SendMail(System.Net.Mail.MailMessage message)
        {
            EmailTracking tracker = new EmailTracking();

            tracker.To = message.To.ToString();
            tracker.From = message.From.ToString();
            tracker.Subject = message.Subject;
            tracker.Body = message.Body;
            
            message.IsBodyHtml = true;

            tracker = EmailTrackingList.AddItem(tracker);
            message.Body = tracker.AppendToken(message.Body);
            mstrLastError = string.Empty;
            mstrLastErrorTitle = mstrLastError;

            if (!Convert.ToBoolean(AppSettings.GetSetting("NotificationsOn")))
            {
                mstrLastError = "Email Notifications are turned off";
                mstrLastErrorTitle = mstrLastError;

                tracker.Sent = false;
                tracker.FailReason = mstrLastError;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);

                return;
            }          
            

            if (message.From.Address.IsNullOrEmpty())
            {
                message.From = AdminReplyAddress;
            }

            try
            {
                message.RemoveDuplicateAddresses();

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                string strSMTP = AppSettings.GetSetting("SMTPServer");


                if (strSMTP.IsNotNullOrEmpty())
                {
                    smtp.Host = strSMTP;
                    smtp.Port = 25;

                }

                //tracking                  

                smtp.Send(message);
                mstrLastError = string.Empty;
                mstrLastErrorTitle = mstrLastError;

                tracker.Sent = true;
                tracker.FailReason = string.Empty;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    mstrLastError = ex.Message;
                    mstrLastErrorTitle = mstrLastError;
                }
                else
                {
                    mstrLastError = ex.GetCompleteErrorMessage();
                    mstrLastErrorTitle = ex.InnerException.Message;

                }
                tracker.Sent = false;
                tracker.FailReason = mstrLastError;
                EmailTrackingList.UpdateItem(tracker.ID, tracker.Sent, tracker.FailReason);

            }
            finally
            {
                //TODO - email logging
            }

        }
        
    }

}
