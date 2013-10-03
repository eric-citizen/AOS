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
    public class SQLObservationProvider : BaseSQLAccess, IObservationProvider
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public SQLObservationProvider()
        {
        }

        #endregion

        public List<Observation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
        {
            lock (base.SyncLock_LOCK)
            {
                List<Observation> items = new List<Observation>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_Observation_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    while (reader.Read())
                    {
                        Observation item = new Observation(reader);
                        items.Add(item);
                    }
                }

                return items;
            }
        }

        public Observation GetItem(int id)
        {
            lock (base.SyncLock_LOCK)
            {
                Observation item = null;

                base.AddParameter("@ObservationID", id);

                using (DbDataReader reader = base.ExecuteReader("czt_Observation_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    if (reader.Read())
                    {
                        item = new Observation(reader);
                    }
                }

                return item;
            }
        }

        public Observation AddItem(Observation item)
        {
            this.AddParameters(item);

            Observation newItem = null;

            using (DbDataReader reader = base.ExecuteReader("czt_Observation_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
            {
                if (reader.Read())
                {
                    newItem = new Observation(reader, true);
                }
            }

            return newItem;
        }

        public void UpdateItem(Observation item)
        {
            base.AddParameter("@ObservationID", item.ObservationID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Observation_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        public int GetCount(string filterExpression)
        {
            filterExpression = filterExpression.EnsureNotNull();
            return base.GetFilteredCount("czt_Observation_GetCount", filterExpression);
        }

        public void DeleteItem(int id)
        {
            base.AddParameter("@ObservationID", id);
            base.ExecuteNonQuery("czt_Observation_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(Observation item)
        {
            base.AddParameter("@Username", item.Username);
            base.AddParameter("@ObserveType", item.ObserveType.ToString());
            base.AddParameter("@ObserveStart", item.ObserveStart);
            base.AddParameter("@ObserveEnd", item.ObserveEnd);
            base.AddParameter("@SchoolID", item.SchoolID);
            base.AddParameter("@GradeID", item.GradeID);
            base.AddParameter("@TeacherName", item.TeacherName);
            base.AddParameter("@ObserverNo", item.ObserverNo);
            base.AddParameter("@ExhibitID", item.ExhibitID);
            base.AddParameter("@Category", item.Category);
            base.AddParameter("@Timer", item.Timer);
            base.AddParameter("@Interval", item.Interval);
            base.AddParameter("@Manual", item.Manual);
            base.AddParameter("@TeacherLogin", item.TeacherLogin);
            base.AddParameter("@TeacherPass", item.TeacherPass);
            base.AddParameter("@StudentPass", item.StudentPass);
            base.AddParameter("@SchoolReview", item.SchoolReview);
            base.AddParameter("@ZooReview", item.ZooReview);
            base.AddParameter("@InReview", item.InReview);
            base.AddParameter("@Deleted", item.Deleted);

        }

    }
}



