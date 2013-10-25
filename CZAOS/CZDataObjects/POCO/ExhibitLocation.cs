using System;
using System.Data;
using System.Data.Common;
using CZDataObjects.Extensions;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class ExhibitLocation  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ExhibitLocation()
			{
			}
    
			public ExhibitLocation(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintExhibitLocationID = record.Get<int>("ExhibitLocationID");
                mintExhibitID = record.Get<int>("ExhibitID");
                mintLocationID = record.Get<int>("LocationID");
                mblnActive = record.Get<bool>("Active");

                if (!isNew)
                {
                    mblnMaskAma = record.Get<string>("MaskAma").FromYesNoString();
                    mblnMaskProf = record.Get<string>("MaskProf").FromYesNoString();
                    mstrLocation = record.Get<string>("Location");
                    mstrExhibit = record.Get<string>("Exhibit");
                }
          
			}

			public ExhibitLocation(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintExhibitLocationID;
        private int mintExhibitID;
        private int mintLocationID;
        private bool mblnActive;
        private string mstrLocation;
        private string mstrExhibit; 
        private bool mblnMaskAma;
        private bool mblnMaskProf;
			
        public string Location
        {
            get
            {
                return mstrLocation;
            }
        }

        public string Exhibit
        {
            get
            {
                return mstrExhibit;
            }
        }

        public int ExhibitLocationID
        {
            get
            {
                return mintExhibitLocationID;
            }            
        }

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

        public bool MaskAma
        {
            get
            {
                return mblnMaskAma;
            }
            set
            {
                mblnMaskAma = value;
            }
        }

        public bool MaskProf
        {
            get
            {
                return mblnMaskProf;
            }
            set
            {
                mblnMaskProf = value;
            }
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



