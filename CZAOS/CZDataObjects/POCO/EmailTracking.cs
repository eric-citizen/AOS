using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class EmailTracking 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public EmailTracking()
			{
			}
    
			public EmailTracking(DbDataReader record, bool isNew = false) // : base(record)
			{
				this.ID = record.Get<int>("ID");
                this.SendDate = record.Get<DateTime>("SendDate");
                this.To = record.Get<string>("To");
                this.From = record.Get<string>("From");
                this.Subject = record.Get<string>("Subject");
                this.Body = record.Get<string>("Body");
                this.UserID = record.Get<Guid>("UserID");
                this.Opened = record.Get<bool>("Opened");
                this.Sent = record.Get<bool>("Sent");
                this.FailReason = record.Get<string>("FailReason");

                if(this.Opened)
                    this.OpenDate = record.Get<DateTime>("OpenDate");

                if (!isNew)
                {
                    this.Username = record.Get<string>("Username");
                    this.DisplayName = record.Get<string>("DisplayName");
                }
                
          
			}			

		#endregion

		#region PROPERTIES		
            
			
        public int ID
        {
            get;private set;            
        }

        public DateTime SendDate
        {
            get;
            private set;
        }

        public string To
        {
            get;
            set; 
        }

        public string From
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }

        public Guid UserID
        {
            get;
            set;
        }

        public string Username
        {
            get;
            private set;
        }

        public string DisplayName
        {
            get;
            private set;
        }

        public bool Opened
        {
            get;
            private set;
        }

        public DateTime OpenDate
        {
            get;
            private set;
        }

        public bool Sent
        {
            get;
            set;
        }

        public string FailReason
        {
            get;
            set;
        }

        public string AppendToken(string body)
        {
            if (this.ID > 0)
            {
                string siteURL = CZAOSCore.basepages.MainBase.SiteUrl;
                siteURL = siteURL.EnsureEndsWith("/");
                body += "<img src='{0}open.aspx?{1}' width='1' height='1'>".FormatWith(siteURL, this.ID);
            }

            return body;
        }

		#endregion

	}

}



