using System;
using System.Data;
using System.Data.Common;
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
                mintBvrCatID = record.Get<int>("BvrCatID");


                if (!isNew)
                {
                    mstrExhibitName = record.Get<string>("ExhibitName");
                    mstrBehavior = record.Get<string>("Behavior");
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



