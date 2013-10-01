using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using CZDataObjects.CustomAttributes;
using CZDataObjects.Extensions;

namespace CZDataObjects
{

	//[Serializable()]
	public class Observation  
	{
        public enum ObservationTypeEnum
        {
            Undefined,
            Professional,
            School
        }
		#region CONSTRUCTORS/DESTRUCTORS

			public Observation()
			{
			}

            public Observation(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintObservationID = record.Get<int>("ObservationID");
                mstrUsername = record.Get<string>("Username");
                
                //mstrObserveType = record.Get<string>("ObserveType");
                mObserveType = CZDOExtensions.ParseEnum<ObservationTypeEnum>(record.Get<string>("ObserveType"));

                mdteObserveStart = record.Get<DateTime>("ObserveStart");
                mdteObserveEnd = record.Get<DateTime>("ObserveEnd");
                
                mintSchoolID = record.Get<int>("SchoolID");
                mintGradeID = record.Get<int>("GradeID");
                mstrTeacherName = record.Get<string>("TeacherName");
                mintObserverNo = record.Get<int>("ObserverNo");
                mintExhibitID = record.Get<int>("ExhibitID");
                mstrCategory = record.Get<string>("Category");
                mblnTimer = record.Get<bool>("Timer");
                mintInterval = record.Get<int>("Interval");
                mblnManual = record.Get<bool>("Manual");
                mstrTeacherLogin = record.Get<string>("TeacherLogin");
                mstrTeacherPass = record.Get<string>("TeacherPass");
                mstrStudentPass = record.Get<string>("StudentPass");
                mblnSchoolReview = record.Get<bool>("SchoolReview");
                mblnZooReview = record.Get<bool>("ZooReview");
                mblnInReview = record.Get<bool>("InReview");
                mblnDeleted = record.Get<bool>("Deleted");

                if (!isNew)
                {
                    //dbo.czt_SchoolDistrict.District, 
                    //dbo.czt_School.School, dbo.czt_Grade.Grade, dbo.czt_Exhibit.Exhibit, dbo.czt_Exhibit.AnimalRegionCode, dbo.czt_AnimalRegion.AnimalRegion, 
                    //dbo.czt_AnimalRegion.RegionName
                    this.District = record.Get<string>("District");
                    this.School = record.Get<string>("School");
                    this.Grade = record.Get<string>("Grade");
                    this.Exhibit = record.Get<string>("Exhibit");
                    this.AnimalRegionCode = record.Get<string>("AnimalRegionCode");
                    this.AnimalRegion = record.Get<string>("AnimalRegion");
                    this.RegionName = record.Get<string>("RegionName");
                    this.DistrictID = record.Get<int>("DistrictID");
                }
          
			}

			public Observation(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintObservationID;
        private string mstrUsername = String.Empty;
        private ObservationTypeEnum mObserveType = ObservationTypeEnum.Undefined;
        private DateTime mdteObserveStart;
        private DateTime mdteObserveEnd;
        private int mintDistrictID;
        private int mintSchoolID;
        private int mintGradeID;
        private string mstrTeacherName = String.Empty;
        private int mintObserverNo;
        private int mintExhibitID;
        private string mstrCategory = String.Empty;
        private bool mblnTimer;
        private int mintInterval;
        private bool mblnManual;
        private string mstrTeacherLogin = String.Empty;
        private string mstrTeacherPass = String.Empty;
        private string mstrStudentPass = String.Empty;
        private bool mblnSchoolReview;
        private bool mblnZooReview;
        private bool mblnInReview;
        private bool mblnDeleted = false;

        public string District
        {
            get;
            private set;
        }

        public string School
        {
            get;
            private set;
        }

        public string Grade
        {
            get;
            private set;
        }

        public string Exhibit
        {
            get;
            private set;
        }


        public string AnimalRegionCode
        {
            get;
            private set;
        }

        public string AnimalRegion
        {
            get;
            private set;
        }

        public string RegionName
        {
            get;
            private set;
        }

        [DataMember(Name = "ObservationID")]
        public int ObservationID
        {
            get
            {
                return mintObservationID;
            }
            set
            {
                mintObservationID = value;
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

        [DataMember(Name = "ObserveType")]
        public ObservationTypeEnum ObserveType
        {
            get
            {
                return mObserveType;
            }
            set
            {
                mObserveType = value;
            }
        }

        public string ObservationTypeName
        {
            get
            {
                return this.ObserveType.ToString();
            }
        }

        [DataMember(Name = "ObserveStart")]
        [DateCompareAttribute("ObserveEnd", "b")]
        public DateTime ObserveStart
        {
            get
            {
                return mdteObserveStart;
            }
            set
            {
                mdteObserveStart = value;
            }
        }

        [DataMember(Name = "ObserveEnd")]
        public DateTime ObserveEnd
        {
            get
            {
                return mdteObserveEnd;
            }
            set
            {
                mdteObserveEnd = value;
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

        [DataMember(Name = "SchoolID")]
        public int SchoolID
        {
            get
            {
                return mintSchoolID;
            }
            set
            {
                mintSchoolID = value;
            }
        }

        [DataMember(Name = "GradeID")]
        public int GradeID
        {
            get
            {
                return mintGradeID;
            }
            set
            {
                mintGradeID = value;
            }
        }

        [DataMember(Name = "TeacherName")]
        public string TeacherName
        {
            get
            {
                return mstrTeacherName;
            }
            set
            {
                mstrTeacherName = value.EnsureNotNull(100);
            }
        }

        [DataMember(Name = "ObserverNo")]
        public int ObserverNo
        {
            get
            {
                return mintObserverNo;
            }
            set
            {
                mintObserverNo = value;
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

        [DataMember(Name = "Category")]
        public string Category
        {
            get
            {
                return mstrCategory;
            }
            set
            {
                mstrCategory = value.EnsureNotNull(20);
            }
        }

        [DataMember(Name = "Timer")]
        public bool Timer
        {
            get
            {
                return mblnTimer;
            }
            set
            {
                mblnTimer = value;
            }
        }

        [DataMember(Name = "Interval")]
        public int Interval
        {
            get
            {
                return mintInterval;
            }
            set
            {
                mintInterval = value;
            }
        }

        [DataMember(Name = "Manual")]
        public bool Manual
        {
            get
            {
                return mblnManual;
            }
            set
            {
                mblnManual = value;
            }
        }

        [DataMember(Name = "TeacherLogin")]
        public string TeacherLogin
        {
            get
            {
                return mstrTeacherLogin;
            }
            set
            {
                mstrTeacherLogin = value.EnsureNotNull(10);
            }
        }

        [DataMember(Name = "TeacherPass")]
        public string TeacherPass
        {
            get
            {
                return mstrTeacherPass;
            }
            set
            {
                mstrTeacherPass = value.EnsureNotNull(10);
            }
        }

        [DataMember(Name = "StudentPass")]
        public string StudentPass
        {
            get
            {
                return mstrStudentPass;
            }
            set
            {
                mstrStudentPass = value.EnsureNotNull(10);
            }
        }

        [DataMember(Name = "SchoolReview")]
        public bool SchoolReview
        {
            get
            {
                return mblnSchoolReview;
            }
            set
            {
                mblnSchoolReview = value;
            }
        }

        [DataMember(Name = "ZooReview")]
        public bool ZooReview
        {
            get
            {
                return mblnZooReview;
            }
            set
            {
                mblnZooReview = value;
            }
        }

        [DataMember(Name = "InReview")]
        public bool InReview
        {
            get
            {
                return mblnInReview;
            }
            set
            {
                mblnInReview = value;
            }
        }

        [DataMember(Name = "Deleted")]
        public bool Deleted
        {
            get
            {
                return mblnDeleted;
            }
            set
            {
                mblnDeleted = value;
            }
        }

		#endregion

	}

}



