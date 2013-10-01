using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace CZDataObjects
{

	//[Serializable()]
	public class WeatherCondition   
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public WeatherCondition()
			{
			}
    
			public WeatherCondition(DbDataReader record) // : base(record)
			{
				mintWeatherID = record.Get<int>("WeatherID");
                mstrWeather = record.Get<string>("Weather");
                mblnActive = record.Get<bool>("Active");
          
			}

			public WeatherCondition(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintWeatherID;
        private string mstrWeather = String.Empty;
        private bool mblnActive;
			
        [DataMember(Name = "WeatherID")]
public int WeatherID
        {
            get
            {
                return mintWeatherID;
            }            
        }

        [DataMember(Name = "Weather")]
public string Weather
        {
            get
            {
                return mstrWeather;
            }
            set
            {
                mstrWeather = value.EnsureNotNull(100);
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

		#endregion

	}

}



