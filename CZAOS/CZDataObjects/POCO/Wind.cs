using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace CZDataObjects
{

	//[Serializable()]
	public class Wind   
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Wind()
			{
			}
    
			public Wind(DbDataReader record) // : base(record)
			{
				mintWindID = record.Get<int>("WindID");
                mstrWind = record.Get<string>("Wind");
                mblnActive = record.Get<bool>("Active");          
			}

			public Wind(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintWindID;
        private string mstrWind = String.Empty;
        private bool mblnActive;
			
        [DataMember(Name = "WindID")]
public int WindID
        {
            get
            {
                return mintWindID;
            }            
        }

        [DataMember(Name = "Description")]
public string Description
        {
            get
            {
                return mstrWind;
            }
            set
            {
                mstrWind = value.EnsureNotNull(100);
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



