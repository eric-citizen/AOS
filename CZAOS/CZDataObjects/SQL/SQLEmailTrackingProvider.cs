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
	public class SQLEmailTrackingProvider : BaseSQLAccess, IEmailTrackingProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public SQLEmailTrackingProvider()
			{
			}   			

		#endregion

        public List<EmailTracking> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<EmailTracking> items = new List<EmailTracking>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_EmailTracking_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
                        EmailTracking item = new EmailTracking(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

        public EmailTracking GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                EmailTracking item = null;                

                base.AddParameter("@Id", id);

                using (DbDataReader reader = base.ExecuteReader("czt_EmailTracking_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
                        item = new EmailTracking(reader);							
					}
				}

                return item;
            }
		}

        public EmailTracking AddItem(EmailTracking item)
		{
            base.AddParameter("@To", item.To);
            base.AddParameter("@From", item.From);
            base.AddParameter("@Subject", item.Subject);
            base.AddParameter("@Body", item.Body);
            base.AddParameter("@UserID", item.UserID);            

            EmailTracking newItem = null;            

            using (DbDataReader reader = base.ExecuteReader("czt_EmailTracking_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
                    newItem = new EmailTracking(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(int id)
		{
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_EmailTracking_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}
        public void UpdateItem(int id, bool sent, string failReason)
        {
            base.AddParameter("@Id", id);
            base.AddParameter("@Sent", sent);
            base.AddParameter("@FailReason", failReason);

            base.ExecuteNonQuery("czt_EmailTracking_UpdateSend", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }


		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
            return base.GetFilteredCount("czt_EmailTracking_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_EmailTracking_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }		

	}
}   

	       
	    
	   