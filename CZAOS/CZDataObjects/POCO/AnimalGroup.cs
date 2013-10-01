using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace CZDataObjects
{

    //[Serializable()]
    public class AnimalGroup
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public AnimalGroup()
        {
        }

        public AnimalGroup(DbDataReader record) // : base(record)
        {
            mintAnimalGroupID = record.Get<int>("AnimalGroupID");
            mintGrpID = record.Get<int>("GrpID");
            mintObservationID = record.Get<int>("ObservationID");
            mstrGrpName = record.Get<string>("GrpName");
            mblnDeleted = record.Get<bool>("Deleted");

        }



        #endregion

        #region PROPERTIES

        private int mintAnimalGroupID;
        private int mintGrpID;
        private int mintObservationID;
        private string mstrGrpName = String.Empty;
        private bool mblnDeleted;

        [DataMember(Name = "AnimalGroupID")]
        public int AnimalGroupID
        {
            get
            {
                return mintAnimalGroupID;
            }
            private set
            {
                mintAnimalGroupID = value;
            }
        }

        [DataMember(Name = "GrpID")]
        public int GrpID
        {
            get
            {
                return mintGrpID;
            }
            set
            {
                mintGrpID = value;
            }
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

        [DataMember(Name = "GrpName")]
        public string GrpName
        {
            get
            {
                return mstrGrpName;
            }
            set
            {
                mstrGrpName = value.EnsureNotNull(20);
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



