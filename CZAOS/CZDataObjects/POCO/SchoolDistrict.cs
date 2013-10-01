using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class SchoolDistrict  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SchoolDistrict()
			{
			}
    
			public SchoolDistrict(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintDistrictID = record.Get<int>("DistrictID");
                mstrDistrict = record.Get<string>("District");
                mblnActive = record.Get<bool>("Active");

                if(!isNew)
                    this.SchoolCount = record.Get<int>("SchoolCount"); 
          
			}

			public SchoolDistrict(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintDistrictID;
        private string mstrDistrict = String.Empty;
        private bool mblnActive;
			
        public int DistrictID
        {
            get
            {
                return mintDistrictID;
            }            
        }

        public int SchoolCount
        {
            get;private set;
        }

        public string District
        {
            get
            {
                return mstrDistrict;
            }
            set
            {
                mstrDistrict = value.EnsureNotNull(100);
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

		#endregion

	}

}



