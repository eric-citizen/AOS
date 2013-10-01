using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace CZAOSMail.Mail
{
    public class EmailMessage : MailMessage
    {

        #region " CONSTRUCTOR "


        public EmailMessage()
            : base()
        {

        }

        #endregion

        #region PROPERTIES

        private string mstrLastError = string.Empty;
        private string mstrSMTPServer = string.Empty;
        private string mstrSMTPServerLogin = string.Empty;
        private string mstrSMTPServerPassword = string.Empty;
        private int mintPort = 25;
        //private int mintAuthenticate = 1;

        private int mintConnectionTimeout = 60;
        public string LastError
        {
            get { return mstrLastError; }
        }

        public string SMTPServer
        {
            get { return mstrSMTPServer; }
            set { mstrSMTPServer = value; }
        }

        public string SMTPServerLogin
        {
            get { return mstrSMTPServerLogin; }
            set { mstrSMTPServerLogin = value; }
        }

        public string SMTPServerPassword
        {
            get { return mstrSMTPServerPassword; }
            set { mstrSMTPServerPassword = value; }
        }

        public int SMTPPort
        {
            get { return mintPort; }
            set { mintPort = value; }
        }

        public int ConnectionTimeout
        {
            get { return mintConnectionTimeout; }
            set { mintConnectionTimeout = value; }
        }

        public bool EnableSsl
        {
            get;
            set;
        }

        #endregion

        public bool Send()
        {

            SmtpClient smtp = default(SmtpClient);

            if (this.SMTPServer.Length > 0)
            {
                smtp = new SmtpClient(this.SMTPServer);
                smtp.Port = this.SMTPPort;

            }
            else
            {
                smtp = new SmtpClient();

            }

            if (this.SMTPServer == "127.0.0.1")
            {
                smtp.Credentials = CredentialCache.DefaultNetworkCredentials;

            }
            else if (this.SMTPServer.Length > 0 && string.IsNullOrEmpty(this.SMTPServerLogin) && string.IsNullOrEmpty(this.SMTPServerPassword))
            {
                smtp.Credentials = CredentialCache.DefaultNetworkCredentials;

            }
            else if (this.SMTPServer.Length > 0)
            {
                smtp.Credentials = new NetworkCredential(this.SMTPServerLogin, this.SMTPServerPassword);

            }

            try
            {
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                //Me.To.Add(New System.Net.Mail.MailAddress("ktilley@thirtyonegifts.com"))

                smtp.Send(this);

                mstrLastError = "";

                return true;

            }
            catch (Exception Ex)
            {
                mstrLastError = Ex.ToString();
                return false;

            }

        }

        public void RemoveDuplicateAddresses()
        {
            MailAddressCollection toAddresses = new MailAddressCollection();
            MailAddressCollection ccAddresses = new MailAddressCollection();
            MailAddressCollection bccAddresses = new MailAddressCollection();

            if (this.To.Count > 0)
            {
                toAddresses.Add(this.To.ToString());
            }

            if (this.CC.Count > 0)
            {
                ccAddresses.Add(this.CC.ToString());
            }

            if (this.Bcc.Count > 0)
            {
                bccAddresses.Add(this.Bcc.ToString());
            }

            this.To.Clear();
            this.CC.Clear();
            this.Bcc.Clear();

            StringCollection dupeAddresses = new StringCollection();

            //TO

            foreach (MailAddress address in toAddresses)
            {
                if (!dupeAddresses.Contains(address.Address.ToLower()))
                {
                    this.To.Add(address);
                    dupeAddresses.Add(address.Address.ToLower());
                }

            }

            //CC

            foreach (MailAddress address in ccAddresses)
            {
                if (!dupeAddresses.Contains(address.Address.ToLower()))
                {
                    this.CC.Add(address);
                    dupeAddresses.Add(address.Address.ToLower());
                }

            }

            //BCC

            foreach (MailAddress address in bccAddresses)
            {
                if (!dupeAddresses.Contains(address.Address.ToLower()))
                {
                    this.Bcc.Add(address);
                    dupeAddresses.Add(address.Address.ToLower());
                }

            }

            dupeAddresses.Clear();
            dupeAddresses = null;

        }

    }


}
