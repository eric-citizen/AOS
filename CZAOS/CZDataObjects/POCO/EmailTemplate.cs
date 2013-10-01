using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace CZDataObjects
{
	[Serializable()]
	public class EmailTemplate   
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public EmailTemplate()
			{
			}
    
			public EmailTemplate(DbDataReader record) // : base(record)
			{
				mintID = record.Get<int>("ID");
                mblnActive = record.Get<bool>("Active");
                mstrBody = record.Get<string>("Body");
                mstrSubject = record.Get<string>("Subject");
                mstrKey = record.Get<string>("Key");
                mstrInstructionalText = record.Get<string>("InstructionalText");
          
			}			

		#endregion

		#region PROPERTIES
			
        private int mintID;
        private bool mblnActive;
        private string mstrBody = String.Empty;
        private string mstrKey = String.Empty;
        private string mstrSubject = String.Empty;
        private string mstrInstructionalText = String.Empty;
			
        [DataMember(Name = "ID")]
        public int ID
        {
            get
            {
                return mintID;
            }            
        }

        [DataMember(Name = "Active")]
        public bool Active
        {
            get
            {
                return mblnActive;
            }
            set
            {
                mblnActive = value;
            }
        }

        [DataMember(Name = "Body")]
        public string Body
        {
            get
            {
                return mstrBody;
            }
            set
            {
                mstrBody = value;
            }
        }

        [DataMember(Name = "Subject")]
        public string Subject
        {
            get
            {
                return mstrSubject;
            }
            set
            {
                mstrSubject = value.EnsureNotNull(100);
            }
        }

        [DataMember(Name = "Key")]
        public string Key
        {
            get
            {
                return mstrKey;
            }
            set
            {
                mstrKey = value.EnsureNotNull(50);
            }
        }

        [DataMember(Name = "InstructionalText")]
        public string InstructionalText
        {
            get
            {
                return mstrInstructionalText;
            }
            set
            {
                mstrInstructionalText = value;
            }
        }

		#endregion

        public void AddVariable(string token, string value)
        {
            if(token.StartsWith("[") && token.EndsWith("]"))
            {
                this.Subject = this.Subject.Replace(token, value);
                this.Body = this.Body.Replace(token, value);
            }
        }

	}

}



