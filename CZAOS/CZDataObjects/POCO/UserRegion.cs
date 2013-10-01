using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace CZDataObjects
{

	//[Serializable()]
	public class UserRegion   
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public UserRegion()
			{
			}
    
			public UserRegion(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintUserRegionID = record.Get<int>("UserRegionID");
                mstrUsername = record.Get<string>("Username");
                mstrAnimalRegionCode = record.Get<string>("AnimalRegionCode");
                mblnActive = record.Get<bool>("Active");

                if (!isNew)
                {
                    mstrAnimalRegion = record.Get<string>("AnimalRegion");
                    mstrRegionName = record.Get<string>("RegionName");
                }
          
			}

			public UserRegion(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintUserRegionID;
        private string mstrUsername = String.Empty;
        private string mstrAnimalRegionCode = String.Empty;
        private bool mblnActive;
        private string mstrAnimalRegion = String.Empty;
        private string mstrRegionName = String.Empty;

        [DataMember(Name = "UserRegionID")]
public int UserRegionID
        {
            get
            {
                return mintUserRegionID;
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

        [DataMember(Name = "AnimalRegionCode")]
public string AnimalRegionCode
        {
            get
            {
                return mstrAnimalRegionCode;
            }
            set
            {
                mstrAnimalRegionCode = value.EnsureNotNull(3);
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

        [DataMember(Name = "AnimalRegion")]
        public string AnimalRegion
        {
            get
            {
                return mstrAnimalRegion;
            }
            private set
            {
                mstrAnimalRegion = value.EnsureNotNull();
            }
        }

        [DataMember(Name = "RegionName")]
        public string RegionName
        {
            get
            {
                return mstrRegionName;
            }
            private set
            {
                mstrRegionName = value.EnsureNotNull();
            }
        }
		#endregion

	}

}



