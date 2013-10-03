using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using KT.Extensions;


namespace CZDataObjects
{
	public class SQLSysCodeProvider : BaseSQLAccess, ISysCodeProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLSysCodeProvider()
			{
			}   			

		#endregion

		public List<SysCode> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<SysCode> items = new List<SysCode>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("cms_SysCodes_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						SysCode item = new SysCode(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

        //public SysCode GetItem(int id)
        //{
        //    lock(base.SyncLock_LOCK)
        //    {
        //        SysCode item = null;                

        //        base.AddParameter("@Id", id);

        //        using (DbDataReader reader = base.ExecuteReader("cms_SysCodes_Get",
        //                        System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
        //        {
        //            if (reader.Read())
        //            {
        //                item = new SysCode(reader);							
        //            }
        //        }

        //        return item;
        //    }
        //}

		public SysCode AddItem(SysCode item)
		{
			this.AddParameters(item); 

			SysCode newItem = null;

			using (DbDataReader reader = base.ExecuteReader("cms_SysCodes_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new SysCode(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(SysCode item)
		{
			base.AddParameter("@Id", item.Id);
            base.AddParameter("@CodeId", item.CodeID);
            this.AddParameters(item);

            base.ExecuteNonQuery("cms_SysCodes_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("cms_SysCodes_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("cms_SysCodes_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(SysCode item)
        {
            base.AddParameter("@ParentId",item.ParentId);            
            base.AddParameter("@Value",item.Value);
            base.AddParameter("@Description",item.Description);
            base.AddParameter("@SortOrder",item.SortOrder);
            base.AddParameter("@DataTypeId",item.DataTypeId);         
        }

	}
}   

	       
	    
	   