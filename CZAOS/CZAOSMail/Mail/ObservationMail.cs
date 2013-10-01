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
using CZAOSCore.Logging;
using CZBizObjects;
using CZDataObjects;

namespace CZAOSMail.Mail
{
    public class ObservationMail : SiteEmailBase
    {
        public void SendNewObservationEmail(Observation obs)
        {
            UserList users = UserList.GetActiveProfessionalUsers();
            UserList newMails = new UserList();
            UserList sendList = new UserList();
            ObserverList observers = ObserverList.GetActive(obs.ObservationID);
            UserList observerUsers = new UserList();

            const string templateKey = "new-pro-observation";

            foreach (CZUser user in users)
            {
                if (user.NewEmail)
                    newMails.Add(user);
            }

            foreach (Observer observer in observers)
            {
                if (observer.ObserveEmail)
                {
                    CZUser oUser = UserList.GetUser(observer.Username);

                    if (!sendList.ContainsUser(observer.Username))
                        sendList.Add(oUser);
                }
            }

            foreach (CZUser user in newMails)
            {
                UserRegionList regions = UserRegionList.GetActive(user.Username);

                if (regions.ContainsAnimalRegion(obs.AnimalRegionCode))
                {
                    if (!sendList.ContainsUser(user.Username))
                        sendList.Add(user);
                }

            }

            if (sendList.Count > 0)
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();
                tokens.Add("[OBSERVATIONID]", obs.ObservationID.ToString());
                tokens.Add("[DATE]", obs.ObserveStart.ToShortDateString());
                tokens.Add("[STARTTIME]", obs.ObserveStart.ToShortTimeString());
                tokens.Add("[ENDTIME]", obs.ObserveEnd.ToShortTimeString());
                tokens.Add("[EXHIBIT]", obs.Exhibit);

                this.SendTemplatedEmail(templateKey, tokens, sendList);
            }            

        }

        private bool SendTemplatedEmail(string templateKey, Dictionary<string, string> tokens, UserList recipients)
        {
            EmailTemplate template = EmailTemplateList.Get(templateKey);

            if (template != null)
            {
                foreach (string token in tokens.Keys)
                {
                    template.AddVariable(token, tokens[token]);
                }

                foreach (CZUser user in recipients)
                {
                    EmailMessage mail = new EmailMessage();

                    mail.To.Add(new MailAddress(user.EmailAddress, user.DisplayName));                    
                    mail.Subject = template.Subject;
                    mail.Body = template.Body;

                    mail.From = new MailAddress(user.EmailAddress, user.DisplayName);

                    base.SendMail(mail, template.Body);                                       
                }

                return base.LastError.IsNullOrEmpty();

            }
            else
            {
                Logger.Log(LogTarget.Object, MessageLevel.Warning, "Email with a template key of '{0}' was not found in the database.".FormatWith(templateKey));
                return false;
            }
        }

    }

}
