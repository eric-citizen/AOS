using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CZAOSCore
{
    public static class CacheManager
    {
        #region Standard Manager

        private static CacheManagerInstance Instance;

        static CacheManager()
        {
            Instance = new CacheManagerInstance();
            //ManagerConfiguration.ReconfigureRequested += new EventHandler(Instance.ReconfigureRequested);
            Instance.Configure();
        }

        private class CacheManagerInstance : InitializedInstanceBase //SimpleManagerBase<ICache>
        {
            //public CacheManagerInstance()
            //    : base("CacheManager")
            //{

            //}

            protected override bool OnInitialize()
            {
                return Provider.Initialize();
            }
        }

        #endregion

        public static void Initialize()
        {
            Instance.Initialize();
        }

        private static bool Compress
        {
            get
            {
                return false; //return Instance.Settings.FlagAttribute("CompressObjects", false); 
            }
        }

        private static bool Enabled { get { return Instance.Initialized; } }

        private const int defaultCacheLifeSpanInMinutes = 10;

        /// <summary>
        /// Removes the item from cache of the given type using the given key value
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="objectType"></param>
        /// <param name="objectKey"></param>
        public static void RemoveFromCache(Type objectType, object objectKey)
        {
            Instance.Initialize();

            if (Enabled && objectType != null && objectKey != null)
            {
                string cacheKey = GetCacheKey(objectType, objectKey);
                Instance.Provider.RemoveFromCache(cacheKey);

                if (HttpContext.Current != null && HttpContext.Current.Items.Contains(cacheKey))
                {
                    HttpContext.Current.Items.Remove(cacheKey);
                }
            }
        }



        /// <summary>
        /// Adds the given object to the web cache with a custom sliding expiration
        /// </summary>
        /// <param name="o"></param>
        /// <param name="objectKey"></param>     
        public static void CacheItem(object o, object objectKey, TimeSpan slidingExpiration)
        {
            Instance.Initialize();

            if (o == null || objectKey == null)
                return; // Can't cache a null.

            if (Enabled)
            {
                string cacheKey = GetCacheKey(o.GetType(), objectKey);

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = o;
                }

                byte[] data = SerializationHelper.SerializeToBinary(o, Compress);
                Instance.Provider.CacheItem(data, cacheKey, slidingExpiration);

            }
        }

        /// <summary>
        /// Adds the given object to the web cache with the default sliding expiration of 2 minutes.
        /// </summary>
        /// <param name="o">The object to store in the cache</param>
        /// <param name="objectKey">The unique key (data element, for example an items tloc)</param>
        public static void CacheItem(object o, object objectKey)
        {
            Instance.Initialize();

            if (Enabled)
            {
                CacheItem(o, objectKey, TimeSpan.FromMinutes(defaultCacheLifeSpanInMinutes));
            }
        }


        /// <summary>
        /// Retrieve an object from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        public static T GetFromCache<T>(object objectKey)
        {
            Instance.Initialize();

            if (Enabled && objectKey != null)
            {
                T value = default(T);
                string cacheKey = GetCacheKey(typeof(T), objectKey);

                // Check Http Context first since its faster
                if (HttpContext.Current != null && HttpContext.Current.Items.Contains(cacheKey))
                {
                    value = (T)HttpContext.Current.Items[cacheKey];
                }
                else
                {
                    byte[] data = Instance.Provider.GetFromCache(cacheKey);

                    if (data != null && data.Length > 0)
                    {
                        value = SerializationHelper.Deserialize<T>(data, Compress);

                        // Add to current context for faster repeated access
                        if (HttpContext.Current != null)
                            HttpContext.Current.Items.Add(cacheKey, value);
                    }
                }

                return value;
            }

            return default(T);
        }

        public static List<T> GetCacheItems<T>(string prefix)
        {
            Instance.Initialize();

            if (Enabled)
            {
                List<ICacheItem> items = Instance.Provider.GetCacheItems();
                List<T> returnValues = new List<T>();

                foreach (ICacheItem item in items)
                {
                    if (item.Value != null && item.Value.Length > 0 && item.Key.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase))
                    {
                        T value = default(T);
                        value = SerializationHelper.Deserialize<T>(item.Value, Compress);
                        returnValues.Add(value);
                    }
                }

                return returnValues;
            }

            return default(List<T>);
        }

        public static object GetFromCache(object objectKey, Type objectType)
        {
            Instance.Initialize();

            if (Enabled && objectKey != null && objectType != null)
            {
                object value = null;
                string cacheKey = GetCacheKey(objectType, objectKey);

                // Check Http Context first since its faster
                if (HttpContext.Current != null && HttpContext.Current.Items.Contains(cacheKey))
                {
                    value = HttpContext.Current.Items[cacheKey];
                }
                else
                {
                    byte[] data = Instance.Provider.GetFromCache(cacheKey);

                    if (data != null && data.Length > 0)
                    {
                        value = SerializationHelper.DeserializeObject(data, Compress);

                        // Add to current context for faster repeated access
                        if (HttpContext.Current != null)
                            HttpContext.Current.Items.Add(cacheKey, value);
                    }
                }

                return value;
            }

            return null;
        }

        /// <summary>
        /// Generates a key that should be unique for this app instance for a given object.
        /// Composes the key based on the object's type and the given value key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        public static string GetCacheKey(Type type, object objectKey)
        {
            return string.Format("{1}:{0}", type.GUID, objectKey.ToString().Trim().ToLower());
        }

        public static void Flush()
        {
            Instance.Initialize();

            if (Enabled)
            {
                Instance.Provider.Flush();
            }
        }

        public static void RemoveGroupStartsWith(string startsWith)
        {
            Instance.Initialize();

            if (Enabled)
            {
                Instance.Provider.RemoveGroupStartsWith(startsWith);
            }
        }

        public static void RemoveGroupContains(string contains)
        {
            Instance.Initialize();

            if (Enabled)
            {
                Instance.Provider.RemoveGroupContains(contains);
            }
        }

        public static int ObjectCount()
        {
            Instance.Initialize();

            if (Enabled)
            {
                return Instance.Provider.ObjectCount();
            }

            return 0;
        }
    }
}
