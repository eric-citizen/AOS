using System;
using System.Data;
using System.Data.Common;
using System.Web;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
	public class ExhibitBehavior  
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ExhibitBehavior()
			{
			}
    
			public ExhibitBehavior(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintExhibitBehaviorID = record.Get<int>("ExhibitBehaviorID");
                mintExhibitID = record.Get<int>("ExhibitID");
                mintBehaviorID = record.Get<int>("BehaviorID");
                mblnActive = record.Get<bool>("Active");


                if (!isNew)
                {
                    mstrExhibitName = HttpUtility.HtmlDecode(record.Get<string>("ExhibitName"));
                    mstrBehavior = HttpUtility.HtmlDecode(record.Get<string>("Behavior"));
                    mstrBehaviorCode = HttpUtility.HtmlDecode(record.Get<string>("BehaviorCode"));
                    mintBvrCatID = record.Get<int>("BvrCatID");
                }
          
			}

			public ExhibitBehavior(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintExhibitBehaviorID;
        private int mintExhibitID;
        private int mintBehaviorID;
        private bool mblnActive;
        private string mstrExhibitName;
        private string mstrBehavior;
        private string mstrBehaviorCode;
        private int mintBvrCatID;

        public string ExhibitName
        {
            get
            {
                return mstrExhibitName;
            }            
        }

        public string Behavior
        {
            get
            {
                return mstrBehavior;
            }
        }

        public string BehaviorCode
        {
            get
            {
                return mstrBehaviorCode;
            }
        }

        public int ExhibitBehaviorID
        {
            get
            {
                return mintExhibitBehaviorID;
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

        public int BehaviorID
        {
            get
            {
                return mintBehaviorID;
            }
            set
            {
                mintBehaviorID = value;
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

        public int BvrCatID
        {
            get
            {
                return mintBvrCatID;
            }
            set
            {
                mintBvrCatID = value;
            }
        }

		#endregion

	}

}



