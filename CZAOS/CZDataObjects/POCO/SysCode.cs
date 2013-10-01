using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{
	//[Serializable()]
	public class SysCode
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SysCode()
			{
			}
    
			public SysCode(DbDataReader record) // : base(record)
			{
				mintId = record.Get<int>("Id");
                //mintParentId = record.Get<int>("ParentId");
                mintCodeID = record.Get<int>("CodeID");
                mstrValue = record.Get<string>("Value");
                mstrDescription = record.Get<string>("Description");
                mintSortOrder = record.Get<int>("SortOrder");
                mintDataTypeId = record.Get<int>("DataTypeId");
          
			}			

		#endregion

		#region PROPERTIES
			
        private int mintId;        
        private int mintCodeID;
        private string mstrValue = String.Empty;
        private string mstrDescription = String.Empty;
        private int mintSortOrder;
        private int mintDataTypeId;
			
        [DataMember(Name = "Id")]
public int Id
        {
            get
            {
                return mintId;
            }
            private set
            {
                mintId = value;
            }
        }

        [DataMember(Name = "ParentId")]
public int ParentId
        {
            get
            {
                return 1;
            }            
        }

        [DataMember(Name = "CodeID")]
public int CodeID
        {
            get
            {
                return mintCodeID;
            }
            set
            {
                mintCodeID = value;
            }
        }

        [DataMember(Name = "Value")]
public string Value
        {
            get
            {
                return mstrValue;
            }
            set
            {
                mstrValue = value.EnsureNotNull(50);
            }
        }

        [DataMember(Name = "Description")]
public string Description
        {
            get
            {
                return mstrDescription;
            }
            set
            {
                mstrDescription = value.EnsureNotNull(250);
            }
        }

        [DataMember(Name = "SortOrder")]
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

        [DataMember(Name = "DataTypeId")]
public int DataTypeId
        {
            get
            {
                return mintDataTypeId;
            }
            set
            {
                mintDataTypeId = value;
            }
        }

		#endregion

	}

}



