using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
//using Integral.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace CZAOSCore
{
    /// <summary>
    /// Enables short-lived object caching using the HTTP web cache.
    /// NOTE: Client ID will get ignored within this provider currently since the web cache is limited to the current app domain by default.
    /// </summary>
    public class IISWebCache : ICache
    {

        public void RemoveFromCache(string objectKey)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Cache.Remove(objectKey);
        }

        public void CacheItem(byte[] data, string objectKey, TimeSpan slidingExpiration)
        {
            InternalCacheItem citem = new InternalCacheItem();
           
            citem.Value = data;
            citem.Key = objectKey;
            citem.SlidingExpiration = slidingExpiration;

            CacheItem(citem);
        }

        public byte[] GetFromCache(string objectKey)
        {
            ICacheItem item = GetCacheItem(objectKey);
            byte[] value = null;
            if (item != null)
                value = item.Value;
            return value;
        }

        public int ObjectCount()
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Cache.Count;
            else
                return 0;
        }

        public void Flush()
        {
            // Essentially you just have to manually remove everything...
            if (HttpContext.Current != null)
            {

                Cache cache = HttpContext.Current.Cache;

                // Enumerate all the keys first... 
                List<string> keysToDrop = new List<string>();
                foreach (DictionaryEntry item in cache)
                {
                    keysToDrop.Add(item.Key as string);
                }

                // Now you can try to drop them
                foreach (string key in keysToDrop)
                {
                    cache.Remove(key);
                }
            }
        }

        public IISWebCache() { }

        public bool Initialize()
        {
            return true;
        }


        public void CacheItem(ICacheItem item)
        {
            if (HttpContext.Current != null && item != null)
            {
                if (item.AbsoluteExpiration != null)
                    HttpContext.Current.Cache.Add(item.Key, item, null, item.AbsoluteExpiration.Value, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                else
                    HttpContext.Current.Cache.Add(item.Key, item, null, Cache.NoAbsoluteExpiration, item.SlidingExpiration ?? TimeSpan.FromMinutes(5), CacheItemPriority.Normal, null);
            }
        }

        public ICacheItem GetCacheItem(string objectKey)
        {
            if (HttpContext.Current != null)
                return (ICacheItem)HttpContext.Current.Cache.Get(objectKey);
            else
                return null;
        }

        public List<ICacheItem> GetCacheItems()
        {
            List<ICacheItem> items = new List<ICacheItem>();
            Cache cache = HttpContext.Current.Cache;

            foreach (DictionaryEntry item in cache)
            {
                if (item.Value is ICacheItem)
                {
                    items.Add(item.Value as ICacheItem);
                }
            }

            return items;
        }

        public void RemoveGroup(string groupName)
        {
            // Essentially you just have to manually remove everything...
            if (HttpContext.Current != null)
            {

                Cache cache = HttpContext.Current.Cache;

                // Enumerate all the keys first... 
                List<string> keysToDrop = new List<string>();
                foreach (DictionaryEntry item in cache)
                {
                    ICacheItem citem = item.Value as ICacheItem;
                    if (citem != null && citem.Group.Equals(groupName, StringComparison.InvariantCultureIgnoreCase))
                        keysToDrop.Add(item.Key as string);
                }

                // Now you can try to drop them
                foreach (string key in keysToDrop)
                {
                    cache.Remove(key);
                }
            }
        }

        public void RemoveGroupStartsWith(string startsWith)
        {
            // Essentially you just have to manually remove everything...
            if (HttpContext.Current != null)
            {

                Cache cache = HttpContext.Current.Cache;

                // Enumerate all the keys first... 
                List<string> keysToDrop = new List<string>();
                foreach (DictionaryEntry item in cache)
                {
                    ICacheItem citem = item.Value as ICacheItem;
                    if (citem != null && citem.Key.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase))
                        keysToDrop.Add(item.Key as string);
                }

                // Now you can try to drop them
                foreach (string key in keysToDrop)
                {
                    cache.Remove(key);
                }
            }
        }

        public void RemoveGroupContains(string contains)
        {
            // Essentially you just have to manually remove everything...
            if (HttpContext.Current != null)
            {

                Cache cache = HttpContext.Current.Cache;

                // Enumerate all the keys first... 
                List<string> keysToDrop = new List<string>();
                foreach (DictionaryEntry item in cache)
                {
                    ICacheItem citem = item.Value as ICacheItem;
                    if (citem != null && citem.Key.Contains(contains))
                        keysToDrop.Add(item.Key as string);
                }

                // Now you can try to drop them
                foreach (string key in keysToDrop)
                {
                    cache.Remove(key);
                }
            }
        }

    }

}