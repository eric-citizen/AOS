using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KT.Extensions;
using System.Data;
using System.Data.Common;


namespace CZDataObjects
{
	//[Serializable()]
	public class ObservationRecord 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ObservationRecord()
			{
			}
    
			public ObservationRecord(DbDataReader record) 
			{
				this.ObservationRecordID = record.Get<int>("ObservationRecordID");
                mintObservationID = record.Get<int>("ObservationID");
                mstrUsername = record.Get<string>("Username");
                mintZooID = record.Get<string>("ZooID");
                //mintAnimalID = record.Get<int>("AnimalID");
                mstrBvrCat = record.Get<string>("BvrCat");
                mstrBvrCatCode = record.Get<string>("BvrCatCode");
                mstrBehavior = record.Get<string>("Behavior");
                mstrBehaviorCode = record.Get<string>("BehaviorCode");
                mintLocationID = record.Get<int>("LocationID");
                mdteObserverTime = record.Get<DateTime>("ObserverTime");
                mblnDeleted = record.Get<bool>("Deleted");
                mblnFlagged = record.Get<bool>("Flagged");
          
			}	
						
			
		#endregion

		#region PROPERTIES
			
       
        private int mintObservationID;
        private string mstrUsername = String.Empty;
        private int mintAnimalID;
        private string mintZooID = String.Empty;
        private string mstrBvrCat = String.Empty;
        private string mstrBvrCatCode = String.Empty;
        private string mstrBehavior = String.Empty;
        private string mstrBehaviorCode = String.Empty;
        private int mintLocationID;
        private DateTime mdteObserverTime;
        private bool mblnDeleted;
        private bool mblnFlagged;
			
        public int ObservationRecordID
        {
            get;private set;           
        }

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

        //public int AnimalID
        //{
        //    get
        //    {
        //        return mintAnimalID;
        //    }
        //    set
        //    {
        //        mintAnimalID = value;
        //    }
        //}

        public string ZooID
        {
            get
            {
                return mintZooID;
            }
            set
            {
                mintZooID = value;
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
                return mstrBvrCatCode;
            }
            set
            {
                mstrBvrCatCode = value.EnsureNotNull(2);
            }
        }

        public string Behavior
        {
            get
            {
                return mstrBehavior;
            }
            set
            {
                mstrBehavior = value.EnsureNotNull(100);
            }
        }

        public string BehaviorCode
        {
            get
            {
                return mstrBehaviorCode;
            }
            set
            {
                mstrBehaviorCode = value.EnsureNotNull(2);
            }
        }

        public int LocationID
        {
            get
            {
                return mintLocationID;
            }
            set
            {
                mintLocationID = value;
            }
        }

        public DateTime ObserverTime
        {
            get
            {
                return mdteObserverTime.ToLocalTime();
            }
            set
            {
                mdteObserverTime = value;
            }
        }

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

        public bool Flagged
        {
            get
            {
                return mblnFlagged;
            }
            set
            {
                mblnFlagged = value;
            }
        }

		#endregion

	}

}



