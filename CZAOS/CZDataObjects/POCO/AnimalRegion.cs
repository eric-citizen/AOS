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
	public class AnimalRegion  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public AnimalRegion()
			{
			}
    
			public AnimalRegion(DbDataReader record, bool isNew = false) // : base(record)
			{
                mstrAnimalRegionCode = record.Get<string>("AnimalRegionCode").EnsureNotNull();
                mstrAnimalRegion = record.Get<string>("AnimalRegion").EnsureNotNull();
                mstrRegionName = record.Get<string>("RegionName").EnsureNotNull();
                mblnActive = record.Get<bool>("Active");

                if (!isNew) { 
                    mintAnimalCount = record.Get<int>("AnimalCount");
                    this.ExhibitCount = record.Get<int>("ExhibitCount");
                }

			}

			public AnimalRegion(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private string mstrAnimalRegionCode = string.Empty;
        private string mstrAnimalRegion = string.Empty;
        private string mstrRegionName = string.Empty;
        private bool mblnActive;
		private int mintAnimalCount;

        [DataMember(Name = "AnimalCount")]
        public int AnimalCount
        {
            get
            {
                return mintAnimalCount;
            }
        }

        [DataMember(Name = "AnimalRegionCode")]
        public string AnimalRegionCode
        {
            get
            {
                return mstrAnimalRegionCode.ToUpper();
            }
            set
            {
                mstrAnimalRegionCode = value.EnsureNotNull(3).ToUpper();
            }
        }

        [DataMember(Name = "AnimalRegionName")]
        public string AnimalRegionName
        {
            get
            {
                return mstrAnimalRegion;
            }
            set
            {
                mstrAnimalRegion = value.EnsureNotNull(100);
            }
        }

        [DataMember(Name = "RegionName")]
        public string RegionName
        {
            get
            {
                return mstrRegionName;
            }
            set
            {
                mstrRegionName = value.EnsureNotNull(100);
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

        public int ExhibitCount
        {
            get;
            private set;
        }
		#endregion

	}

}



