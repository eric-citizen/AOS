using System;
using System.Data;
using System.Data.Common;
using System.Web;
using KT.Extensions;
using CZDataObjects.Extensions;
using CZDataObjects.Interfaces;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class Location : ISortable 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Location()
			{
			}
    
			public Location(DbDataReader record) // : base(record)
			{
				mintLocationID = record.Get<int>("LocationID");
                mstrDescription = HttpUtility.HtmlDecode(record.Get<string>("Description"));
                mstrLocationCode = HttpUtility.HtmlDecode(record.Get<string>("LocationCode"));
                mblnMaskAma = record.Get<string>("MaskAma").FromYesNoString();
                mblnMaskProf = record.Get<string>("MaskProf").FromYesNoString();
                mintSortOrder = record.Get<int>("SortOrder");
                mblnActive = record.Get<bool>("Active");
          
			}

			public Location(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintLocationID;
        private string mstrDescription = String.Empty;
        private string mstrLocationCode = String.Empty;
        private bool mblnMaskAma;
        private bool mblnMaskProf;
        private int mintSortOrder;
        private bool mblnActive;

        public int SortItemID
        {
            get
            {
                return mintLocationID;
            }
        }

        public int LocationID
        {
            get
            {
                return mintLocationID;
            }            
        }

        public string Description
        {
            get
            {
                return mstrDescription;
            }
            set
            {
                mstrDescription = value.EnsureNotNull(100);
            }
        }

        public string LocationCode
        {
            get
            {
                return mstrLocationCode;
            }
            set
            {
                mstrLocationCode = value.EnsureNotNull(2);
            }
        }

        public bool MaskAma
        {
            get
            {
                return mblnMaskAma;
            }
            set
            {
                mblnMaskAma = value;
            }
        }

        public bool MaskProf
        {
            get
            {
                return mblnMaskProf;
            }
            set
            {
                mblnMaskProf = value;
            }
        }

        public int SortOrder
        {
            get
            {
                return mintSortOrder;
            }
            set
            {
                mintSortOrder = value;
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



