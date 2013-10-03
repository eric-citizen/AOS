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
	public class SQLEmailTemplateProvider : BaseSQLAccess, IEmailTemplateProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLEmailTemplateProvider()
			{
			}   			

		#endregion

		public List<EmailTemplate> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<EmailTemplate> items = new List<EmailTemplate>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_EmailTemplate_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						EmailTemplate item = new EmailTemplate(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public EmailTemplate GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                EmailTemplate item = null;                

                base.AddParameter("@Id", id);

				using (DbDataReader reader = base.ExecuteReader("czt_EmailTemplate_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new EmailTemplate(reader);							
					}
				}

                return item;
            }
		}

		public EmailTemplate AddItem(EmailTemplate item)
		{
			this.AddParameters(item); 

			EmailTemplate newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_EmailTemplate_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new EmailTemplate(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(EmailTemplate item)
		{
			base.AddParameter("@Id", item.ID );
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_EmailTemplate_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_EmailTemplate_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_EmailTemplate_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(EmailTemplate item)
        {
            base.AddParameter("@Active",item.Active);
            base.AddParameter("@Body",item.Body);
            base.AddParameter("@Subject", item.Subject);
            base.AddParameter("@Key",item.Key);
            base.AddParameter("@InstructionalText",item.InstructionalText);            
        }

	}
}   

	       
	    
	   