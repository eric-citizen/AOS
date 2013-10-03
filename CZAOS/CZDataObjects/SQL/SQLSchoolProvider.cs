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
    public class SQLSchoolProvider : BaseSQLAccess, ISchoolProvider
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public SQLSchoolProvider()
        {
        }

        #endregion

        public List<School> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            lock (base.SyncLock_LOCK)
            {
                List<School> items = new List<School>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_School_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    while (reader.Read())
                    {
                        School item = new School(reader);
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        public School GetItem(int id)
        {
            lock (base.SyncLock_LOCK)
            {
                School item = null;

                base.AddParameter("@SchoolId", id);

                using (DbDataReader reader = base.ExecuteReader("czt_School_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    if (reader.Read())
                    {
                        item = new School(reader);
                    }
                }

                return item;
            }
        }

        public School AddItem(School item)
        {
            this.AddParameters(item);

            School newItem = null;

            using (DbDataReader reader = base.ExecuteReader("czt_School_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
            {
                if (reader.Read())
                {
                    newItem = new School(reader, true);
                }
            }

            return newItem;
        }

        public void UpdateItem(School item)
        {
            base.AddParameter("@SchoolId", item.SchoolID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_School_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        public int GetCount(string filterExpression)
        {
            filterExpression = filterExpression.EnsureNotNull();
            return base.GetFilteredCount("czt_School_GetCount", filterExpression);
        }

        public void DeleteItem(int id)
        {
            base.AddParameter("@SchoolId", id);
            base.ExecuteNonQuery("czt_School_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(School item)
        {
            base.AddParameter("@School", item.SchoolName);
            base.AddParameter("@DistrictID", item.DistrictID);
            base.AddParameter("@Active", item.Active);

        }

    }
}



