using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KT.Extensions;

namespace CZDataObjects
{

    public class BaseSQLAccess : DatabaseHelper
    {
        protected readonly object SyncLock_LOCK = new object();

        protected int GetFilteredCount(string procedureName, string filterExpression)
        {
            lock (this.SyncLock_LOCK)
            {                
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@filterExpression", filterExpression);
                return (int)base.ExecuteScalar(procedureName, System.Data.CommandType.StoredProcedure);
            }
        }

        protected int GetNonFilteredCount(string procedureName)
        {
            lock (this.SyncLock_LOCK)
            {
                base.AddParameter("@filterExpression", string.Empty);
                return (int)base.ExecuteScalar(procedureName, System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
