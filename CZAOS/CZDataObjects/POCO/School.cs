using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

    [Serializable()]
    [DataContract]
	public class School 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public School()
			{
			}
    
			public School(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintSchoolID = record.Get<int>("SchoolID");
                mstrSchool = record.Get<string>("School");
                mintDistrictID = record.Get<int>("DistrictID");
                mblnActive = record.Get<bool>("Active");

                if(!isNew)
                    mstrDistrictName = record.Get<string>("DistrictName");
          
			}

			public School(DataRow row)  
			{
                throw new NotImplementedException();
			}

		#endregion

		#region PROPERTIES
			
        private int mintSchoolID;        
        private string mstrSchool = String.Empty;
        private int mintDistrictID;
        private bool mblnActive;
        private string mstrDistrictName = String.Empty;

        [DataMember(Name = "SchoolID")]
        public int SchoolID
        {
            get
            {
                return mintSchoolID;
            }
            private set
            {
                mintSchoolID = value;
            }

        }

        [DataMember(Name = "SchoolName")]
        [StringLength(100, MinimumLength = 1)]
        public string SchoolName
        {
            get
            {
                return mstrSchool;
            }
            set
            {
                mstrSchool = value.EnsureNotNull(100);                
            }
        }

        [DataMember(Name = "DistrictName")]
        public string DistrictName
        {
            get
            {
                return mstrDistrictName;
            }
            private set
            {
                mstrDistrictName = value;
            }
        }

        [DataMember(Name = "DistrictID")]
        public int DistrictID
        {
            get
            {
                return mintDistrictID;
            }
            set
            {
                mintDistrictID = value;
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

		#endregion

	}

}



