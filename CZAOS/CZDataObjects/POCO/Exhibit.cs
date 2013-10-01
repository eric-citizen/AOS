using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class Exhibit  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Exhibit()
			{
			}
    
			public Exhibit(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintExhibitID = record.Get<int>("ExhibitID");
                mstrAnimalRegionCode = record.Get<string>("AnimalRegionCode");
                mstrExhibit = record.Get<string>("Exhibit");
                mblnActive = record.Get<bool>("Active");

                if (!isNew)
                {
                    mstrAnimalRegion = record.Get<string>("AnimalRegion");
                    mstrRegionName = record.Get<string>("RegionName");
                    mintBehaviorCount = record.Get<int>("BehaviorCount");
                    mintLocationCount = record.Get<int>("LocationCount");
                }          
			}

			public Exhibit(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintExhibitID;
        private string mstrAnimalRegionCode = String.Empty;
        private string mstrExhibit = String.Empty;
        private bool mblnActive;
        private string mstrAnimalRegion = String.Empty;
        private string mstrRegionName = String.Empty;
        private int mintBehaviorCount;
        private int mintLocationCount;

        public int BehaviorCount
        {
            get
            {
                return mintBehaviorCount;
            }            
        }

        public int LocationCount
        {
            get
            {
                return mintLocationCount;
            }
        }

        public string AnimalRegion
        {
            get
            {
                return mstrAnimalRegion;
            }            
        }

        public string RegionName
        {
            get
            {
                return mstrRegionName;
            }
        }

        public int ExhibitID
        {
            get
            {
                return mintExhibitID;
            }            
        }

        public string AnimalRegionCode
        {
            get
            {
                return mstrAnimalRegionCode;
            }set
            {
                mstrAnimalRegionCode = value.EnsureNotNull(3);
            }
            
        }

        public string ExhibitName
        {
            get
            {
                return mstrExhibit;
            }
            set
            {
                mstrExhibit = value.EnsureNotNull(100);
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



