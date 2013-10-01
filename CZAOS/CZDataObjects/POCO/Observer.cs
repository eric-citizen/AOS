using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{
	//[Serializable()]
	public class Observer   
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Observer()
			{
			}
    
			public Observer(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintObserverID = record.Get<int>("ObserverID");
                mintObservationID = record.Get<int>("ObservationID");
                mstrUsername = record.Get<string>("Username");
                mblnLocked = record.Get<bool>("Locked");
                mblnDeleted = record.Get<bool>("Deleted");

                if (!isNew)
                {
                    this.DisplayName = record.Get<string>("DisplayName");
                    this.ObserveEmail = record.Get<bool>("ObserveEmail");
                    this.NewEmail = record.Get<bool>("NewEmail");
                    this.CompEmail = record.Get<bool>("CompEmail");
                    this.WeekEmail = record.Get<bool>("WeekEmail");

                }
          
			}

			public Observer(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintObserverID;
        private int mintObservationID;
        private string mstrUsername = String.Empty;
        private bool mblnLocked;
        private bool mblnDeleted;
			
        [DataMember(Name = "ObserverID")]
        public int ObserverID
        {
            get
            {
                return mintObserverID;
            }            
        }

        [DataMember(Name = "ObservationID")]
        public int ObservationID
        {
            get
            {
                return mintObservationID;
            }
            set
            {
                mintObservationID = value;
            }
        }

        [DataMember(Name = "Username")]
        public string Username
        {
            get
            {
                return mstrUsername;
            }
            set
            {
                mstrUsername = value.EnsureNotNull(20);
            }
        }

        [DataMember(Name = "Locked")]
        public bool Locked
        {
            get
            {
                return mblnLocked;
            }
            set
            {
                mblnLocked = value;
            }
        }

        [DataMember(Name = "Deleted")]
        public bool Deleted
        {
            get
            {
                return mblnDeleted;
            }
            set
            {
                mblnDeleted = value;
            }
        }

        [DataMember(Name = "DisplayName")]
        public string DisplayName
        {
            get;            
            private set;            
        }

        public bool NewEmail
        {
            get;
            private set;  
        }

        public bool CompEmail
        {
            get;
            private set;  
        }

        public bool ObserveEmail
        {
            get;
            private set;  
        }

        public bool WeekEmail
        {
            get;
            private set;  
        }

		#endregion

	}

}



