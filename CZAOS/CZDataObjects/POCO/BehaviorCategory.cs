using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using CZDataObjects.Extensions;
using CZDataObjects.Interfaces;

namespace CZDataObjects
{

	//[Serializable()]
	public class BehaviorCategory : ISortable 
    {
		#region CONSTRUCTORS/DESTRUCTORS

			public BehaviorCategory()
			{
			}
    
			public BehaviorCategory(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintBvrCatID = record.Get<int>("BvrCatID");
                mstrBvrCat = record.Get<string>("BvrCat");
                mstrBvrCatCode = record.Get<string>("BvrCatCode");
                mstrDescription = record.Get<string>("Description");
                mblnMaskAma = record.Get<string>("MaskAma").FromYesNoString();
                mblnMaskProf = record.Get<string>("MaskProf").FromYesNoString();
                mintSortOrder = record.Get<int>("SortOrder");
                mblnActive = record.Get<bool>("Active");

                if (!isNew)
                    mintBehaviorCount = record.Get<int>("BehaviorCount");
          
			}            

			public BehaviorCategory(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintBvrCatID;
        private string mstrBvrCat = String.Empty;
        private string mstrBvrCatCode = String.Empty;
        private string mstrDescription = String.Empty;
        private bool mblnMaskAma;
        private bool mblnMaskProf;
        private int mintSortOrder;
        private bool mblnActive;
        private int mintBehaviorCount;

        public int BehaviorCount
        {
            get
            {
                return mintBehaviorCount;
            }
            
        }

        public int BvrCatID
        {
            get
            {
                return mintBvrCatID;
            }            
        }

        public string BvrCat
        {
            get
            {
                return mstrBvrCat;
            }
            set
            {
                mstrBvrCat = value.EnsureNotNull(100);
            }
        }

        public string BvrCatCode
        {
            get
            {
                return mstrBvrCatCode.ToUpper();
            }
            set
            {
                mstrBvrCatCode = value.EnsureNotNull(2).ToUpper();
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

        public int SortItemID
        {
            get
            {
                return mintBvrCatID;
            }

        }

	}

}



