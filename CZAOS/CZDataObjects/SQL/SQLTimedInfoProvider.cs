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
	public class SQLTimedInfoProvider : BaseSQLAccess, ITimedInfoProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public SQLTimedInfoProvider()
			{
			}   			

		#endregion

        public List<TimedInfo> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<TimedInfo> items = new List<TimedInfo>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_TimedInfo_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
                        TimedInfo item = new TimedInfo(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

        public TimedInfo GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                TimedInfo item = null;

                base.AddParameter("@TimedInfoId", id);

                using (DbDataReader reader = base.ExecuteReader("czt_TimedInfo_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
                        item = new TimedInfo(reader);							
					}
				}

                return item;
            }
		}

        public TimedInfo AddItem(TimedInfo item)
		{
			this.AddParameters(item);

            TimedInfo newItem = null;

            using (DbDataReader reader = base.ExecuteReader("czt_TimedInfo_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
                    newItem = new TimedInfo(reader);							
				}
			}
			
			return newItem;		
		}

        public void UpdateItem(TimedInfo item)
		{
            base.AddParameter("@TimedInfoId", item.TimedInfoID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_TimedInfo_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
            return base.GetFilteredCount("czt_TimedInfo_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@TimedInfoId", id);
            base.ExecuteNonQuery("czt_TimedInfo_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(TimedInfo item)
        {
            base.AddParameter("@TimeStart", item.TimeStart);
            base.AddParameter("@TimeEnd", item.TimeEnd);
            base.AddParameter("@Interval", item.Interval);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   