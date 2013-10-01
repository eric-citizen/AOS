using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KT.Extensions;
using System.IO;
using System.Web;

namespace CZAOSMail.Mail
{

    public class SiteMessage
    {
        public enum TemplateType
        {
            //ALSO corresponds to the actual template filename 
            pwdReminder
        }


        private const string TemplateLocation = "~/assets/email-templates/";
        public static string GetTemplate(TemplateType type)
        {

            FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(TemplateLocation + type.ToString() + ".txt"));


            if (fi.Exists)
            {
                StreamReader fs = null;

                try
                {
                    fs = fi.OpenText();
                    return fs.ReadToEnd();

                }
                finally
                {
                    fs.Close();

                }

            }
            else
            {
                return string.Empty;

            }

        }

        public SiteMessage(TemplateType template)
        {
            msbrMessage = new StringBuilder(GetTemplate(template));
            mstrSubject = msbrMessage.ToString().Substring(0, msbrMessage.ToString().IndexOf("\n"));

            if (mstrSubject.Contains(SUBJECTSTART))
            {
                mstrSubject = mstrSubject.Replace(SUBJECTSTART, "").Replace(SUBJECTEND, "");
                msbrMessage = msbrMessage.Remove(0, msbrMessage.ToString().IndexOf("\n"));
            }
            else
            {
                mstrSubject = "";

            }

        }

        public SiteMessage(string message)
        {
            msbrMessage = new StringBuilder(message);
            mstrSubject = msbrMessage.ToString().Substring(0, msbrMessage.ToString().IndexOf("\n"));

            if (mstrSubject.Contains(SUBJECTSTART))
            {
                mstrSubject = mstrSubject.Replace(SUBJECTSTART, "").Replace(SUBJECTEND, "");
                msbrMessage = msbrMessage.Remove(0, msbrMessage.ToString().IndexOf("\n"));
            }
            else
            {
                mstrSubject = "";

            }

        }

        private StringBuilder msbrMessage;

        private string mstrSubject = string.Empty;
        private const string SUBJECTSTART = "[SUBJECT:";
        private const string SUBJECTEND = ":SUBJECT]";
        private const string HTMLSTART = "[HTML:";
        private const string HTMLEND = ":HTML]";
        private const string PLAINSTART = "[PLAINTEXT:";

        private const string PLAINEND = ":PLAINTEXT]";
        public string Subject
        {
            get { return mstrSubject; }
        }

        public string MessageBody
        {
            get { return msbrMessage.ToString(); }
        }

        public string HTMLMessageBody
        {
            get { return this.GetHTMLMessage(); }
        }

        public string PlainTextMessageBody
        {
            get { return this.GetPlainTextMessageBody(); }
        }


        public void AddVariable(string key, string value)
        {
            msbrMessage = msbrMessage.Replace(string.Format("<% {0} %>", key), value);

        }


        public void Format(int index, string value)
        {
            if (!string.IsNullOrEmpty(msbrMessage.ToString()))
            {       
                
                msbrMessage = msbrMessage.Replace(string.Format("{0}{1}{2}", (char)123, index, (char)125), value);
            }

        }


        public void FormatSubject(int index, string value)
        {
            if (!string.IsNullOrEmpty(mstrSubject))
            {
                mstrSubject = mstrSubject.Replace(string.Format("{0}{1}{2}", (char)123, index, (char)125), value);
            }

        }


        public void RemoveLine(string key)
        {
            string[] strLines = msbrMessage.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            foreach (string line in strLines)
            {
                if (line.Contains(key))
                {
                    msbrMessage = msbrMessage.Replace(line, "");
                    return;
                }

            }

        }

        private string GetHTMLMessage()
        {

            string strHTMLMessageBody = msbrMessage.ToString();
            // Replace(HTMLSTART, "").Replace(HTMLEND, "").ToString

            strHTMLMessageBody = strHTMLMessageBody.Replace(HTMLSTART, "").Replace(HTMLEND, "").ToString();

            int intPlainStart = strHTMLMessageBody.IndexOf(PLAINSTART);
            int intPlainEnd = strHTMLMessageBody.IndexOf(PLAINEND) + PLAINEND.Length;
            int intPlainLength = intPlainEnd - intPlainStart;

            if (intPlainLength > PLAINEND.Length)
            {
                strHTMLMessageBody = strHTMLMessageBody.Remove(intPlainStart, intPlainLength);
            }

            strHTMLMessageBody = string.Concat("<span style='font-family: verdana;'>", strHTMLMessageBody, "</span>");

            return strHTMLMessageBody.ReplaceLineBreaks();
            //.Replace(ControlChars.NewLine, "<br/>")

        }

        private string GetPlainTextMessageBody()
        {

            //Dim x As String = msbrMessage.ToString
            //x = Regex.Replace(x, "<[^>]+>", "")

            string strPlainTextMessageBody = msbrMessage.ToString();
            // Replace(PLAINSTART, "").Replace(PLAINEND, "").ToString
            strPlainTextMessageBody = strPlainTextMessageBody.Replace(PLAINSTART, "").Replace(PLAINEND, "").ToString();

            int intStart = strPlainTextMessageBody.IndexOf(HTMLSTART);
            int intEnd = strPlainTextMessageBody.IndexOf(HTMLEND) + HTMLEND.Length;
            int intLength = intEnd - intStart;

            if (intLength > HTMLEND.Length)
            {
                strPlainTextMessageBody = strPlainTextMessageBody.Remove(intStart, intLength);
            }

            return strPlainTextMessageBody;

        }

    }

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================

}
