using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZAOSCore
{
    public interface ICache
    {
        void RemoveFromCache(string objectKey);

        /// <summary>
        /// Adds the given object to the cache with a custom sliding expiration
        /// </summary>
        /// <param name="o"></param>
        /// <param name="objectKey"></param>     
        void CacheItem(byte[] data, string objectKey, TimeSpan slidingExpiration);

        /// <summary>
        /// Adds the given object to the cache 
        /// </summary>
        /// <param name="item"></param>
        void CacheItem(ICacheItem item);

        /// <summary>
        /// Retrieve an object from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        byte[] GetFromCache(string objectKey);

        /// <summary>
        /// Retrieves an item and its cache metadata
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        ICacheItem GetCacheItem(string objectKey);

        List<ICacheItem> GetCacheItems();

        /// <summary>
        /// Initialize any required resources for the cache -- returns false if caching could not be initialized
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Gets the number of cache entries for the given tenant
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        int ObjectCount();

        /// <summary>
        /// Removes all cache entries for the given tenant
        /// </summary>
        /// <param name="clientId"></param>
        void Flush();

        /// <summary>
        /// Removes all cache entries with the same group name
        /// </summary>
        /// <param name="groupName"></param>
        void RemoveGroup(string groupName);

        /// <summary>
        /// Removes all cache entries who's key starts with a specified value
        /// </summary>
        /// <param name="groupName"></param>
        void RemoveGroupStartsWith(string startsWith);

        /// <summary>
        /// Removes all cache entries who's key contains the specified value
        /// </summary>
        /// <param name="groupName"></param>
        void RemoveGroupContains(string contains);


    }
}
