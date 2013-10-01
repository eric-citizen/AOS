using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using CZAOSCore;
using CZAOSCore.Logging;
using CZDataObjects;

namespace CZBizObjects
{
    [Serializable()]
    public abstract class BaseList<T> : List<T> 
    {

        public BaseList()
        {
            _cachingEnabled = AppSettings.GetSetting<bool>("SiteCachingEnabled");
            _cacheDuration = AppSettings.GetSetting<int>("SiteCachingMins");
        }

        public BaseList(List<T> list)
        {
            base.AddRange(list);

            _cachingEnabled = AppSettings.GetSetting<bool>("SiteCachingEnabled");
            _cacheDuration = AppSettings.GetSetting<int>("SiteCachingMins");

        }

        private static bool _cachingEnabled = false;
        private static int _cacheDuration = 0;

        private static bool CachingEnabled
        {
            get
            {
                return _cachingEnabled;
            }
        }

        private static int CacheDuration
        {
            get
            {
                return _cacheDuration;
            }
        }

        private static string MyCacheKey
        {
            get
            {
                Type theType = typeof(List<T>);
                return CacheManager.GetCacheKey(theType, ("CZBizObjects_" + theType.Name));
            }
        }

        protected static void AddToCache(List<T> o)
        {
            if (o != null && CachingEnabled)
            {
                CacheManager.CacheItem(o, MyCacheKey, new TimeSpan(0, CacheDuration, 0));                
            }
        }

        //protected static void AddToCache(string key, List<T> o)
        //{
        //    if (o != null && CachingEnabled)
        //    {
        //        CacheManager.CacheItem(o, MyCacheKey + key, new TimeSpan(0, CacheDuration, 0));                
        //    }
        //}

        //protected static void AddToCache(T o)
        //{
        //    if (o != null && CachingEnabled)
        //    {
        //        CacheManager.CacheItem(o, MyCacheKey, new TimeSpan(0, CacheDuration, 0));
        //    }
        //}

        public static void PurgeCacheItems()
        {
            CacheManager.Flush();
        }

        //public static void RemoveCacheItem(T o)
        //{
        //    CacheManager.RemoveFromCache(o.GetType(), MyCacheKey);
        //}

        //public static void RemoveCacheItem(T o, string key)
        //{
        //    CacheManager.RemoveFromCache(o.GetType(), MyCacheKey + key);
        //}

        //public static void RemoveCacheItem(List<T> o, string key)
        //{
        //    CacheManager.RemoveFromCache(o.GetType(), MyCacheKey + key);
        //}

        public static void RemoveCacheList()
        {            
            CacheManager.RemoveFromCache(typeof(List<T>), MyCacheKey);
        }

        //public static void RemoveCacheItems(string startsWith)
        //{
        //    CacheManager.RemoveGroupStartsWith(startsWith);
        //}

        //protected static T GetCacheItem()
        //{
        //    return CacheManager.GetFromCache<T>(MyCacheKey);
        //}

        //protected static T GetCacheItem(string key)
        //{            
        //    return CacheManager.GetFromCache<T>(MyCacheKey + key);
        //}

        protected static List<T> GetCacheList()
        {
            return CacheManager.GetFromCache<List<T>>(MyCacheKey);
        }

        //protected static List<T> GetCacheList(string key)
        //{
        //    return CacheManager.GetFromCache<List<T>>(MyCacheKey + key);
        //}

        //protected static void AddToCache(List<T> o)

        public static void AuditUpdate(T original, T updated, int id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                if (original != null)
                {
                    ChangeLog log = new ChangeLog();

                    log.ChangeType = ChangeLog.LogChangeType.update;

                    log.CreateIdentifier(original, id);

                    CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                    log.UserID = czuser.UserId;
                    log.UserDisplayName = czuser.DisplayName;

                    if (log.CompareObject(original, updated))
                    {
                        ChangeLogList.AddItem(log);
                    }

                }
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "AuditUpdate failed", x, false);
            }           

             
        }

        public static void AuditUpdate(T original, T updated, string id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                if (original != null)
                {
                    ChangeLog log = new ChangeLog();

                    log.ChangeType = ChangeLog.LogChangeType.update;

                    log.CreateIdentifier(original, id);

                    CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                    log.UserID = czuser.UserId;
                    log.UserDisplayName = czuser.DisplayName;

                    if (log.CompareObject(original, updated))
                    {
                        ChangeLogList.AddItem(log);
                    }

                }
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "AuditUpdate failed", x, false);
            }


        }

        public static void Audit(T item, ChangeLog.LogChangeType type, int id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                ChangeLog log = new ChangeLog();

                log.ChangeType = type;

                log.CreateIdentifier(item, id);

                CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                log.UserID = czuser.UserId;
                log.UserDisplayName = czuser.DisplayName;

                ChangeLogList.AddItem(log);
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "Audit create/delete failed", x, false);
            }


        }

        public static void Audit(T item, ChangeLog.LogChangeType type, string id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                ChangeLog log = new ChangeLog();

                log.ChangeType = type;

                log.CreateIdentifier(item, id);

                CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                log.UserID = czuser.UserId;
                log.UserDisplayName = czuser.DisplayName;

                ChangeLogList.AddItem(log);
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "Audit create/delete failed", x, false);
            }


        }

        public static void Audit(Type itemtype, ChangeLog.LogChangeType type, int id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                ChangeLog log = new ChangeLog();

                log.ChangeType = type;

                log.CreateIdentifier(itemtype, id);

                CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                log.UserID = czuser.UserId;
                log.UserDisplayName = czuser.DisplayName;

                ChangeLogList.AddItem(log);
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "Audit create/delete failed", x, false);
            }


        }

        public static void Audit(Type itemtype, ChangeLog.LogChangeType type, string id)
        {
            //Wind original = GetItem(item.WindID);
            try
            {
                ChangeLog log = new ChangeLog();

                log.ChangeType = type;

                log.CreateIdentifier(itemtype, id);

                CZUser czuser = UserList.GetUser(HttpContext.Current.User.Identity.Name);
                log.UserID = czuser.UserId;
                log.UserDisplayName = czuser.DisplayName;

                ChangeLogList.AddItem(log);
            }
            catch (Exception x)
            {
                Logger.LogError(ErrorLevel.Error, "Audit create/delete failed", x, false);
            }


        }

        public class SortManager : BaseSortManager, System.Collections.Generic.IComparer<T>
        {

            //constructor for automatic sorting from gridviews etc

            public SortManager(string sort)
                : base(sort)
            {

            }

            //constructor for manual sorting 

            public SortManager(string sortColumn, SortDirection sortDir)
                : base(sortColumn, sortDir)
            {

            }

            //constructor for manual sorting on multiple fields

            public SortManager(StringCollection sortColumns, SortDirection sortDir)
                : base(sortColumns, sortDir)
            {

            }

            public int Compare(T x, T y)
            {

                return base.Compare(typeof(T), x, y);

            }

        }

        public abstract class BaseSortManager
        {

            //constructor for automatic sorting from gridviews etc

            public BaseSortManager(string sort)
            {
                if (sort.ToUpper().EndsWith(" DESC"))
                {
                    mstrColumnName = sort.ToUpper().Replace(" DESC", "");
                    mSortDirection = SortDirection.Descending;

                }
                else if (sort.ToUpper().EndsWith(" ASC"))
                {
                    mstrColumnName = sort.ToUpper().Replace(" ASC", "");
                    mSortDirection = SortDirection.Ascending;

                }
                else
                {
                    mstrColumnName = sort;
                    mSortDirection = SortDirection.Ascending;

                }

            }

            //constructor for manual sorting 

            public BaseSortManager(string sortColumn, SortDirection sortDir)
            {
                if (sortColumn.ToUpper().EndsWith(" DESC"))
                {
                    mstrColumnName = sortColumn.ToUpper().Replace(" DESC", "");
                }

                if (sortColumn.ToUpper().EndsWith(" ASC"))
                {
                    mstrColumnName = sortColumn.ToUpper().Replace(" ASC", "");
                }

                mstrColumnName = sortColumn;
                mSortDirection = sortDir;

            }

            //constructor for manual sorting on multiple fields

            public BaseSortManager(StringCollection sortColumns, SortDirection sortDir)
            {
                mscolColumnNames = sortColumns;
                mSortDirection = sortDir;

            }

            private string mstrColumnName = string.Empty;
            private StringCollection mscolColumnNames = new StringCollection();

            private SortDirection mSortDirection;
            public int Compare(Type theType, object x, object y)
            {


                if (mscolColumnNames.Count == 0)
                {
                    int intReturn = BaseSortManager.GetCompareResult(mstrColumnName, theType, x, y);

                    if (mSortDirection == SortDirection.Descending)
                    {
                        return -intReturn;
                    }
                    else
                    {
                        return intReturn;
                    }


                }
                else if (mscolColumnNames.Count == 1)
                {
                    int intReturn = BaseSortManager.GetCompareResult(mscolColumnNames[0], theType, x, y);

                    if (mSortDirection == SortDirection.Descending)
                    {
                        return -intReturn;
                    }
                    else
                    {
                        return intReturn;
                    }


                }
                else
                {
                    int intReturn = default(int);

                    foreach (string column in mscolColumnNames)
                    {
                        intReturn = BaseSortManager.GetCompareResult(column, theType, x, y);

                        if (intReturn != 0)
                        {
                            return intReturn;
                        }

                    }

                    return intReturn;

                }

            }

            public static int GetCompareResult(string compareFieldName, Type theType, object x, object y)
            {

                if ((x == null) && (y == null))
                {
                    return 0;

                }
                else if ((x == null))
                {
                    return -1;

                }
                else if ((y == null))
                {
                    return 1;

                }

                PropertyInfo[] myProperties = theType.GetProperties((BindingFlags.Public | BindingFlags.Instance));


                foreach (PropertyInfo pi in myProperties)
                {

                    if (pi.Name.ToLower() == compareFieldName.ToLower())
                    {
                        IComparable comparer = (global::System.IComparable)pi.GetValue(x, null);
                        return comparer.CompareTo(pi.GetValue(y, null));

                    }

                }

                return 0;

            }

        }

    }


}
