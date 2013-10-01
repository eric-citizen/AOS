using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{
    
    [DataContract]
	public class CZUser 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public CZUser()
			{
			}
    
			public CZUser(DbDataReader record, bool isNew = false) 
			{
				mstrUsername = record.Get<string>("Username");
                mstrDisplayName = record.Get<string>("DisplayName");
                mdteExpirationDate = record.Get<DateTime>("ExpirationDate");
                mstrUserType = record.Get<string>("UserType");
                mblnNewEmail = record.Get<bool>("NewEmail");
                mblnCompEmail = record.Get<bool>("CompEmail");
                mblnObserveEmail = record.Get<bool>("ObserveEmail");
                mblnWeekEmail = record.Get<bool>("WeekEmail");
                mblnActive = record.Get<bool>("Active");
                mUserId = record.Get<Guid>("UserId");

                if (!isNew)
                {
                    this.RegionCount = record.Get<int>("RegionCount");
                    this.EmailAddress = record.Get<string>("Email"); 
                }
          
			}

			public CZUser(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private string mstrUsername = string.Empty;
        private string mstrDisplayName = string.Empty;
        private DateTime? mdteExpirationDate;
        private string mstrUserType = string.Empty;
        private bool mblnNewEmail;
        private bool mblnCompEmail;
        private bool mblnObserveEmail;
        private bool mblnWeekEmail;
        private bool mblnActive;
        private Guid mUserId;

        public string EmailAddress
        {
            get;
            private set;
        }
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

        [DataMember(Name = "DisplayName")]
        public string DisplayName
        {
            get
            {
                return mstrDisplayName;
            }
            set
            {
                mstrDisplayName = value.EnsureNotNull(100);
            }
        }

        public DateTime? ExpirationDate
        {
            get
            {
                return mdteExpirationDate;
            }
            set
            {
                mdteExpirationDate = value;
            }
        }

        [StringLength(100, MinimumLength = 1)]
        public string UserType
        {
            get
            {
                return mstrUserType;
            }
            set
            {
                mstrUserType = value.EnsureNotNull(20);
            }
        }

        public bool NewEmail
        {
            get
            {
                return mblnNewEmail;
            }
            set
            {
                mblnNewEmail = value;
            }
        }

        public bool CompEmail
        {
            get
            {
                return mblnCompEmail;
            }
            set
            {
                mblnCompEmail = value;
            }
        }

        public bool ObserveEmail
        {
            get
            {
                return mblnObserveEmail;
            }
            set
            {
                mblnObserveEmail = value;
            }
        }

        public bool WeekEmail
        {
            get
            {
                return mblnWeekEmail;
            }
            set
            {
                mblnWeekEmail = value;
            }
        }

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

        public Guid UserId
        {
            get
            {
                return mUserId;
            }
            set
            {
                mUserId = value;
            }
        }

        public int RegionCount
        {
            get;
            private set;
        }
		#endregion

	}

}



