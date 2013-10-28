using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KT.Extensions;
using System.Data;
using System.Data.Common;


namespace CZDataObjects
{	
	public class Weather 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Weather()
			{
			}
    
			public Weather(DbDataReader record, bool isNew = false)
			{
				mintWeatherID = record.Get<int>("WeatherID");
                mintObservationID = record.Get<int>("ObservationID");
                mstrUsername = record.Get<string>("Username");
                mintWeatherConditionID = record.Get<int>("WeatherConditionID");
                mintTemperature = record.Get<int>("Temperature");
                mintWindID = record.Get<int>("WindID");
                mintCrowdID = record.Get<int>("CrowdID");
                mdteWeatherTime = record.Get<DateTime>("WeatherTime");
                mblnDeleted = record.Get<bool>("Deleted");
                mblnFlagged = record.Get<bool>("Flagged");
          
                //read only
                if (!isNew) {
                    mstrWeatherDescription = record.Get<string>("WeatherDescription");
                    mstrCrowd = record.Get<string>("Crowd");
                    mstrWind = record.Get<string>("Wind");     
                }                         

			}	
						
			
		#endregion

		#region PROPERTIES
			
        private int mintWeatherID;
        private int mintObservationID;
        private string mstrUsername = String.Empty;
        private int mintWeatherConditionID;
        private int mintTemperature;
        private int mintWindID;
        private int mintCrowdID;
        private DateTime mdteWeatherTime;
        private bool mblnDeleted;
        private bool mblnFlagged;

        //read only
        private string mstrWeatherDescription = String.Empty;
        private string mstrCrowd = String.Empty;
        private string mstrWind = String.Empty;

        public string WeatherDescription
        {
            get
            {
                return mstrWeatherDescription;
            }
            private set
            {
                mstrWeatherDescription = value.EnsureNotNull();
            }
        }

        public string Crowd
        {
            get
            {
                return mstrCrowd;
            }
            private set
            {
                mstrCrowd = value.EnsureNotNull();
            }
        }

        public string Wind
        {
            get
            {
                return mstrWind;
            }
            private set
            {
                mstrWind = value.EnsureNotNull();
            }
        }

        public int ID
        {
            get
            {
                return mintWeatherID;
            }
            private set
            {
                mintWeatherID = value;
            }
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

        public int WeatherConditionID
        {
            get
            {
                return mintWeatherConditionID;
            }
            set
            {
                mintWeatherConditionID = value;
            }
        }

        public int Temperature
        {
            get
            {
                return mintTemperature;
            }
            set
            {
                mintTemperature = value;
            }
        }

        public int WindID
        {
            get
            {
                return mintWindID;
            }
            set
            {
                mintWindID = value;
            }
        }

        public int CrowdID
        {
            get
            {
                return mintCrowdID;
            }
            set
            {
                mintCrowdID = value;
            }
        }

        public DateTime WeatherTime
        {
            get
            {
                return mdteWeatherTime;
            }
            set
            {
                mdteWeatherTime = value;
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



