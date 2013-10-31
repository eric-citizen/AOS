using System;
using System.Data;
using System.Data.Common;
using System.Web;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class Crowd 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Crowd()
			{
			}
    
			public Crowd(DbDataReader record) // : base(record)
			{
				mintCrowdID = record.Get<int>("CrowdID");
                mstrCrowd = HttpUtility.HtmlDecode(record.Get<string>("Crowd"));
                mblnActive = record.Get<bool>("Active");
          
			}

			public Crowd(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintCrowdID;
        private string mstrCrowd = String.Empty;
        private bool mblnActive;
			
        public int CrowdID
        {
            get
            {
                return mintCrowdID;
            }
            private set
            {
                mintCrowdID = value;
            }
        }

        public string CrowdName
        {
            get
            {
                return mstrCrowd;
            }
            set
            {
                mstrCrowd = value.EnsureNotNull(100);
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



