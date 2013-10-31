using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{
    //[Serializable()]
    public class AnimalObservation
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public AnimalObservation()
        {
            
        }

        public AnimalObservation(DbDataReader record, bool isNew = false) // : base(record)
        {
            this.AnimalObservationID = record.Get<int>("AnimalObservationID");
            this.ObservationID = record.Get<int>("ObservationID");
            this.GroupID = record.Get<int>("GrpId");
            this.AnimalID = record.Get<int>("AnimalID");
            this.Deleted = record.Get<bool>("Deleted");

            if (!isNew) 
            {
                this.ZooID = record.Get<string>("ZooID");
                this.CommonName = record.Get<string>("CommonName");
                this.HouseName = record.Get<string>("HouseName");
                this.AnimalRegionCode = record.Get<string>("AnimalRegionCode");
            }
                

        }

        #endregion

        #region PROPERTIES


        public int AnimalObservationID
        {
            get;
            private set;
        }

        public string ZooID
        {
            get;
            private set;
        }

        public string CommonName
        {
            get;
            private set;
        }

        [DataMember(Name = "DisplayName")]
        public string DisplayName
        {
            get
            {
                return string.Format("{0} : {1} : {2}", CommonName, HouseName, ZooID);
            }
        }

        public string HouseName
        {
            get;
            private set;
        }

        public string AnimalRegionCode
        {
            get;
            private set;
        }

        public int ObservationID
        {
            get;
            set;

        }

        public int GroupID
        {
            get;
            set;
        }

        public int AnimalID
        {
            get;
            set;
        }

        public bool Deleted
        {
            get;
            set;
        }

        #endregion

    }

	//[Serializable()]
    //public class Grade  
    //{
    //    #region CONSTRUCTORS/DESTRUCTORS

    //        public Grade()
    //        {
    //        }
    
    //        public Grade(DbDataReader record) // : base(record)
    //        {
    //            mintGradeID = record.Get<int>("GradeID");
    //            mstrGrade = record.Get<string>("Grade");
    //            mblnActive = record.Get<bool>("Active");
          
    //        }

    //        public Grade(DataRow row)  
    //        {

    //        }

    //    #endregion

    //    #region PROPERTIES
			
    //    private int mintGradeID;
    //    private string mstrGrade = String.Empty;
    //    private bool mblnActive;
			
    //    public int GradeID
    //    {
    //        get
    //        {
    //            return mintGradeID;
    //        }            
    //    }

    //    public string GradeName
    //    {
    //        get
    //        {
    //            return mstrGrade;
    //        }
    //        set
    //        {
    //            mstrGrade = value.EnsureNotNull(100);
    //        }
    //    }

    //    public bool Active
    //    {
    //        get
    //        {
    //            return mblnActive;
    //        }
    //        set
    //        {
    //            mblnActive = value;
    //        }
    //    }

    //    #endregion

    //}

}



