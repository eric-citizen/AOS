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
	public class Animal  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Animal()
			{
			}
    
			public Animal(DbDataReader record, bool isNew = false) //: base(record)
			{                
				mintAnimalID = record.Get<int>("AnimalID");
                mstrAnimalRegionCode = record.Get<string>("AnimalRegionCode").EnsureNotNull();
                mstrZooID = record.Get<string>("ZooID").EnsureNotNull();
                mstrCommonName = record.Get<string>("CommonName").EnsureNotNull();
                mstrHouseName = record.Get<string>("HouseName").EnsureNotNull();
                mstrScientificName = record.Get<string>("ScientificName").EnsureNotNull();
                mstrGender = record.Get<string>("Gender").EnsureNotNull();
                mstrDOB = record.Get<string>("DOB").EnsureNotNull();
                mstrCZArrival = record.Get<string>("CZArrival").EnsureNotNull();
                mblnActive = record.Get<bool>("Active");
			    mintExhibitID = record.Get<int>("ExhibitID");

                if (!isNew)
                {
                    mstrAnimalRegion = record.Get<string>("AnimalRegion").EnsureNotNull();
                    mstrRegionName = record.Get<string>("RegionName").EnsureNotNull();
                    mblnAnimalRegionActive = record.Get<bool>("AnimalRegionActive");
                }
                

			}

			public Animal(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintAnimalID;
        private string mstrAnimalRegionCode = string.Empty;
        private string mstrZooID = string.Empty;
        private string mstrCommonName = string.Empty;
        private string mstrHouseName = string.Empty;
        private string mstrScientificName = string.Empty;
        private string mstrGender = string.Empty;
        private string mstrDOB = string.Empty;
        private string mstrCZArrival = string.Empty;
        private bool mblnActive;
        private int mintExhibitID;

        private string mstrAnimalRegion = string.Empty;
        private string mstrRegionName = string.Empty;
        private bool mblnAnimalRegionActive;

        [DataMember(Name = "AnimalRegionActive")]
        public bool AnimalRegionActive
        {
            get
            {
                return mblnAnimalRegionActive;
            }
        }

        [DataMember(Name = "AnimalRegion")]
        public string AnimalRegion 
        {
            get
            {
                return mstrRegionName;
            }            
        }

        [DataMember(Name = "RegionName")]
        public string RegionName
        {
            get
            {
                return mstrRegionName;
            }
        }

        [DataMember(Name = "AnimalID")]
        public int AnimalID
        {
            get
            {
                return mintAnimalID;
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

        [DataMember(Name = "ZooID")]
        public string ZooID
        {
            get
            {
                return mstrZooID;
            }
            set
            {
                mstrZooID = value.EnsureNotNull(50);
            }
        }

        [DataMember(Name = "CommonName")]
        public string CommonName
        {
            get
            {
                return mstrCommonName;
            }
            set
            {
                mstrCommonName = value.EnsureNotNull(100);
            }
        }

        [DataMember(Name = "HouseName")]
        public string HouseName
        {
            get
            {
                return mstrHouseName;
            }
            set
            {
                mstrHouseName = value.EnsureNotNull(150);
            }
        }

        [DataMember(Name = "ScientificName")]
        public string ScientificName
        {
            get
            {
                return mstrScientificName;
            }
            set
            {
                mstrScientificName = value.EnsureNotNull(150);
            }
        }

        [DataMember(Name = "Gender")]
        public string Gender
        {
            get
            {
                return mstrGender;
            }
            set
            {
                mstrGender = value.EnsureNotNull(50);
            }
        }

        [DataMember(Name = "DOB")]
        public string DOB
        {
            get
            {
                return mstrDOB;
            }
            set
            {
                mstrDOB = value.EnsureNotNull(50);
            }
        }

        [DataMember(Name = "CZArrival")]
        public string CZArrival
        {
            get
            {
                return mstrCZArrival;
            }
            set
            {
                mstrCZArrival = value.EnsureNotNull(50);
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

        [DataMember(Name = "ExhibitID")]
        public int ExhibitID
        {
            get
            {
                return mintExhibitID;
            }
            set
            {
                mintExhibitID = value;
            }
        }

		#endregion

	}

}



