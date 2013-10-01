using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KT.Extensions;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using CZAOSCore;

namespace CZAOSMail.Mail
{
    public class Notifications : SiteEmailBase
    {
        private const char ADDRESS_DELIMITER = ',';

        //public static string GetAdminAddresses()
        //{

        //    string[] admins = Roles.GetUsersInRole("Master"); //TODO
        //    StringBuilder sbrAddresses = new StringBuilder();

        //    if (admins.Length == 0)
        //    {
        //        return string.Empty;
        //    }


        //    foreach (string admin in admins)
        //    {
                 
        //        MembershipUser user = Membership.GetUser(admin);
        //        //SiteAdmin profile = SiteAdminList.GetItemByUserName(admin);

        //        if (user.IsApproved && user.Email.IsValidEmailAddress()) // && profile.ReceiveEmails)
        //        {
        //            sbrAddresses.Append(user.Email).Append(ADDRESS_DELIMITER);
        //        }

        //    }

        //    return sbrAddresses.ToString().TrimEnd(ADDRESS_DELIMITER);

        //}

        //public static List<string> GetAdminAddressList()
        //{

        //    return new List<string>(GetAdminAddresses().Split(ADDRESS_DELIMITER));

        //}

        public bool SendUserCredentials(string userName, System.Web.UI.Control control)
        {            
            MembershipUser member = Membership.GetUser(userName);

            if ((member != null))
            {

                System.Web.UI.WebControls.MailDefinition mailDefinition = new System.Web.UI.WebControls.MailDefinition();
                System.Collections.Specialized.ListDictionary replacements = new System.Collections.Specialized.ListDictionary();

                mailDefinition.From = AdminReplyAddress.Address;
                mailDefinition.BodyFileName = "~/assets/email-templates/password-reminder.htm";

                replacements.Add("<% Username %>", userName);
               

                if (!member.IsLockedOut)
                {                   
                    replacements.Add("<% password %>", member.GetPassword());
                }
                else
                {                    
                    replacements.Add("<% password %>", "Cannot retrieve password: account locked out.");
                    mstrLastError = "Cannot retrieve password: account locked out.";

                }

                MailMessage mail = mailDefinition.CreateMailMessage(member.Email, replacements, control);
                mail.IsBodyHtml = true;
                mail.Subject = AppSettings.GetSetting("EntityTitle") + " Website: Requested Information";

                base.SendMail(mail);

            }
            else
            {
                mstrLastError = "User with username '{0}' not found.".FormatWith(userName);

            }

            return mstrLastError.IsNullOrEmpty();

        }

        public void SendAdminEmail(string subject, string msg)
        {
            MailAddress recipientAddress = new MailAddress(AppSettings.GetSetting("SiteAdminAddress"));
            EmailMessage mail = new EmailMessage();

            mail.To.Add(recipientAddress);
            mail.Subject = subject;
            base.SendMail(mail, msg);

        }

        //public bool WebsiteContactEmail(string senderName, string senderAddress, string senderCity, string senderState, string senderZip, string senderEmail, string senderPhone, string message, string userIP, string recipients = "")
        //{

        //    SiteMessage customMessage = new SiteMessage(Resources.EmailTemplates.WebsiteComment);

        //    customMessage.FormatSubject(0, AppSettings.GetSetting("EntityTitle"));
        //    customMessage.Format(0, AppSettings.GetSetting("EntityTitle"));

        //    customMessage.AddVariable("Name", senderName);
        //    customMessage.AddVariable("Address", senderAddress);
        //    customMessage.AddVariable("City", senderCity);
        //    customMessage.AddVariable("State", senderState);
        //    customMessage.AddVariable("Zip", senderZip);
        //    customMessage.AddVariable("Email", senderEmail);
        //    customMessage.AddVariable("Phone", senderPhone);

        //    customMessage.AddVariable("Comments", message);

        //    System.Web.UI.WebControls.HyperLink linkBan = new System.Web.UI.WebControls.HyperLink();
        //    linkBan.NavigateUrl = "{0}/cms-admin/site_security/?autobanip={1}".FormatWith(base.GetBaseUrl(), userIP);
        //    linkBan.Text = userIP;
        //    customMessage.AddVariable("IP", linkBan.GetHTML());

        //    customMessage.AddVariable("Date", Now.ToString("f"));


        //    EmailMessage mail = new EmailMessage();


        //    if (string.IsNullOrEmpty(recipients))
        //    {
        //        string strAdminAddress = GetAdminAddresses();

        //        if (strAdminAddress.Length > 0)
        //        {
        //            mail.To.Add(strAdminAddress);
        //        }

        //    }
        //    else
        //    {
        //        KT.System.Web.Email.AddressHelper validator = new KT.System.Web.Email.AddressHelper(recipients);
        //        mail.To.Add(validator.AddressString);

        //    }

        //    base.SendMail(mail, AppSettings.GetSetting("EntityTitle") + " Website Contact Email", customMessage.PlainTextMessageBody, customMessage.HTMLMessageBody);

        //    if (base.LastError.IsNullOrEmpty)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public bool SendQuickMail(string toAddresses, string subject, string fromName, string fromAddress, string message)
        {

            EmailMessage mail = new EmailMessage();
            AddressHelper addressHelper = new AddressHelper(toAddresses);

            foreach (MailAddress address in addressHelper.AddressCollection)
            {
                mail.To.Add(address);
            }

            mail.From = new MailAddress(fromAddress, fromName);
            //(New System.Net.Mail.MailAddress("web@vutech-ruff.com"))

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;

            base.SendMail(mail, message);
            bool blnSendOk = base.LastError.IsNullOrEmpty();

            if (!blnSendOk)
            {
                mstrLastError = base.LastError;
            }

            return blnSendOk;

        }

        public bool SendQuickNetMail(string toAddresses, string subject, string fromName, string fromAddress, string message, bool isHtml = false)
        {

            System.Net.Mail.MailMessage mail = new MailMessage();

            AddressHelper addressHelper = new AddressHelper(toAddresses);
            foreach (MailAddress address in addressHelper.AddressCollection)
            {
                mail.To.Add(address);
            }

            mail.From = new MailAddress(fromAddress, fromName);            

            mail.Subject = subject;
            mail.IsBodyHtml = isHtml;
            mail.Body = message;

            base.SendMail(mail);
            bool blnSendOk = base.LastError.IsNullOrEmpty();

            if (!blnSendOk)
            {
                mstrLastError = base.LastError;
            }

            return blnSendOk;

        }


        //public bool SendTemplateEmail(string recipients, string subject, string bodyFileName, System.Collections.Specialized.ListDictionary replacements, System.Web.UI.Control control)
        //{

        //    System.Web.UI.WebControls.MailDefinition mailDefinition = new System.Web.UI.WebControls.MailDefinition();

        //    mailDefinition.From = AdminReplyAddress.Address;
        //    mailDefinition.BodyFileName = bodyFileName;

        //    if (replacements.Count == 0)
        //    {
        //        mstrLastError = "replacements.Count is 0";
        //    }

        //    MailMessage mail = mailDefinition.CreateMailMessage(recipients, replacements, control);
        //    mail.IsBodyHtml = true;

        //    base.SendMail(mail, subject);

        //    return mstrLastError.IsNullOrEmpty();

        //}

        //public void SendAdminMessage(DataObjects.SiteAdmin currentUser, CMSEngine.DataObjects.AdminMessage message)
        //{
        //    EmailMessage mail = new EmailMessage();

        //    mail.IsBodyHtml = true;
        //    mail.Subject = message.MessageSubject;
        //    mail.Body = message.Message;

        //    mail.To.Add(new System.Net.Mail.MailAddress(currentUser.Email));
        //    mail.From = AdminReplyAddress;

        //    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

        //    try
        //    {
        //        smtp.Send(mail);

        //    }
        //    catch (Exception ex)
        //    {
        //        mstrLastError = ex.Message;

        //    }

        //}

        protected string GetBrowserDetails()
        {

            System.Web.HttpContext current = System.Web.HttpContext.Current;

            if ((current == null))
            {
                return string.Empty;


            }
            else
            {
                StringBuilder sbr = new StringBuilder();


                try
                {
                    sbr.Append("Browser Details:").AppendLine();
                    sbr.AppendFormat("Browser.AOL: {0}", current.Request.Browser.AOL).AppendLine();
                    sbr.AppendFormat("Browser.Browser: {0}", current.Request.Browser.Browser).AppendLine();
                    sbr.AppendFormat("Browser.Cookies: {0}", current.Request.Browser.Cookies).AppendLine();
                    sbr.AppendFormat("Browser.Crawler: {0}", current.Request.Browser.Crawler).AppendLine();
                    sbr.AppendFormat("Browser.Frames: {0}", current.Request.Browser.Frames).AppendLine();
                    sbr.AppendFormat("Browser.JavaScript: {0}", current.Request.Browser.EcmaScriptVersion).AppendLine();
                    sbr.AppendFormat("Browser.Type: {0}", current.Request.Browser.Type).AppendLine();
                    sbr.AppendFormat("Browser.MinorVersion: {0}", current.Request.Browser.MinorVersion).AppendLine();
                    sbr.AppendFormat("Browser.Version: {0}", current.Request.Browser.Version).AppendLine();
                    sbr.AppendFormat("Browser.Win32: {0}", current.Request.Browser.Win32).AppendLine();
                    //sbr.AppendFormat("Browser.ScreenBitDepth: {0}", current.Request.Browser.ScreenBitDepth).AppendLine()
                    //sbr.AppendFormat("Browser.ScreenPixelsHeight: {0}", current.Request.Browser.ScreenPixelsHeight).AppendLine()
                    //sbr.AppendFormat("Browser.ScreenPixelsWidth: {0}", current.Request.Browser.ScreenPixelsWidth).AppendLine()

                    return sbr.ToString();

                }
                catch (Exception ex)
                {
                    return ex.Message;

                }

            }

        }

        //public void SendGoogleMail(string toAddresses, string subject, string fromName, string fromAddress, string message)
        //{
        //    System.Net.Mail.MailMessage mail = new MailMessage();

        //    AddressHelper addressHelper = new AddressHelper(toAddresses);
        //    foreach (MailAddress address in addressHelper.AddressCollection)
        //    {
        //        mail.To.Add(address);
        //    }

        //    mail.From = new MailAddress(fromAddress, fromName);

        //    mail.Subject = subject;
        //    mail.IsBodyHtml = true;
        //    mail.Body = message;

        //    base.SendGoogleMail(mail, subject);
        //    bool blnSendOk = base.LastError.IsNullOrEmpty();

        //    if (!blnSendOk)
        //    {
        //        mstrLastError = base.LastError;
        //    }            
        //}

        //public void SendMail(string toAddresses, string subject, string fromName, string fromAddress, string body)
        //{
        //    //string toAddresses, string subject, string fromName, string fromAddress, string message
        //    //System.Net.Mail.MailMessage mail = new MailMessage();
        //    EmailMessage message = new EmailMessage();

        //    AddressHelper addressHelper = new AddressHelper(toAddresses);
        //    foreach (MailAddress address in addressHelper.AddressCollection)
        //    {
        //        message.To.Add(address);
        //    }

        //    message.From = new MailAddress(fromAddress, fromName);

        //    message.Subject = subject;
        //    message.IsBodyHtml = true;
        //    message.Body = body;

        //    message.SMTPServer = "smtp.gmail.com";
        //    message.SMTPServerLogin = "kenscotttilley@gmail.com";
        //    message.SMTPServerPassword = "43054ng171fx";
        //    message.SMTPPort = 587;
        //    message.EnableSsl = true;

        //    base.SendMail(message, subject, body);
        //    bool blnSendOk = base.LastError.IsNullOrEmpty();

        //    if (!blnSendOk)
        //    {
        //        mstrLastError = base.LastError;
        //    }
        //}

    }


}
