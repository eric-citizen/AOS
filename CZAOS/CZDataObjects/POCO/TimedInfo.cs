using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class TimedInfo 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public TimedInfo()
			{
			}
    
			public TimedInfo(DbDataReader record) // : base(record)
			{
                mintTimedInfoID = record.Get<int>("TimedInfoID");
                mTimeStart = record.Get<DateTime>("TimeStart");
                mTimeEnd = record.Get<DateTime>("TimeEnd");
                //mintInterval = record.Get<int>("Interval");
                mblnActive = record.Get<bool>("Active");
          
			}

            public TimedInfo(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES

            private int mintTimedInfoID;
            private DateTime mTimeStart;
            private DateTime mTimeEnd;
            //private int mintInterval;
            private bool mblnActive;
			
        public int TimedInfoID
        {
            get
            {
                return mintTimedInfoID;
            }
            private set
            {
                mintTimedInfoID = value;
            }
        }

        public DateTime TimeStart
        {
            get
            {
                return mTimeStart;
            }
            set
            {
                mTimeStart = new DateTime(2013, 1, 1);
                mTimeStart = mTimeStart.AddHours(value.Hour);
                mTimeStart = mTimeStart.AddMinutes(value.Minute);
            }
        }

        public DateTime TimeEnd
        {
            get
            {
                return mTimeEnd;
            }
            set
            {
                mTimeEnd = new DateTime(2013, 1, 1);
                mTimeEnd = mTimeEnd.AddHours(value.Hour);
                mTimeEnd = mTimeEnd.AddMinutes(value.Minute);
            }
        }

        public int Interval
        {
            get
            {
                //if (mTimeEnd.IsAfterDate(mTimeStart))
                //{
                //    mintInterval = (int)mTimeEnd.Subtract(mTimeStart).TotalMinutes;
                //}
                //else
                //{
                //    mintInterval
                //}
                return (int)mTimeEnd.Subtract(mTimeStart).TotalMinutes;;
            }
            //private set
            //{
            //    mintInterval = value;
            //}
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



