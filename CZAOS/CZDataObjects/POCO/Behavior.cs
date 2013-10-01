using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using CZDataObjects.Interfaces;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

	//[Serializable()]
    //[DataContract]
	public class Behavior : ISortable
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Behavior()
			{
			}
    
			public Behavior(DbDataReader record, bool isNew = false) // : base(record)
			{
				mintBehaviorID = record.Get<int>("BehaviorID");
                mstrBehavior = record.Get<string>("Behavior");
                mintBvrCatID = record.Get<int>("BvrCatID");
                mstrBehaviorCode = record.Get<string>("BehaviorCode");
                mstrDescription = record.Get<string>("Description");
                mintSortOrder = record.Get<int>("SortOrder");
                mblnActive = record.Get<bool>("Active");

                if (!isNew)
                {
                    mstrBvrCat = record.Get<string>("BvrCat");
                    mstrBvrCatCode = record.Get<string>("BvrCatCode");
                    mblnCategoryActive = record.Get<bool>("CategoryActive");
                }


			}

			public Behavior(DataRow row)  
			{

			}

		#endregion


      //  [BehaviorID]
      //,[Behavior]
      //,[BvrCatID]
      //,[BehaviorCode]
      //,[Description]
      //,[SortOrder]
      //,[Active]
      //,[BvrCat]
      //,[BvrCatCode]
      //,[CategoryActive]

		#region PROPERTIES
			
        private int mintBehaviorID;
        private string mstrBehavior = String.Empty;
        private int mintBvrCatID;
        private string mstrBehaviorCode = String.Empty;
        private string mstrDescription = String.Empty;
        private int mintSortOrder = 999;
        private bool mblnActive;

        private string mstrBvrCat = String.Empty;
        private string mstrBvrCatCode = String.Empty;
        private bool mblnCategoryActive;

        public string BehaviorCategory
        {
            get
            {
                return mstrBvrCat;
            }            
        }

        public string BehaviorCategoryCode
        {
            get
            {
                return mstrBvrCatCode;
            }
        }

        public bool CategoryActive
        {
            get
            {
                return mblnCategoryActive;
            }
        }

        public int BehaviorID
        {
            get
            {
                return mintBehaviorID;
            }            
        }

        public string BehaviorName
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

        public string BehaviorCode
        {
            get
            {
                return mstrBehaviorCode;
            }
            set
            {
                mstrBehaviorCode = value.EnsureNotNull(3);
            }
        }

        public string Description
        {
            get
            {
                return mstrDescription;
            }
            set
            {
                mstrDescription = value.EnsureNotNull(500);
            }
        }

        public int SortOrder
        {
            get
            {
                return mintSortOrder;
            }
            set
            {
                mintSortOrder = value;
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

        public int SortItemID
        {
            get
            {
                return this.BehaviorID;
            }

        }

	}

}



