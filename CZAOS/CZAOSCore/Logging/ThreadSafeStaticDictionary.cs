using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CZAOSCore.Logging
{
    public class ThreadSafeStaticDictionary<K, T> : IDisposable
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private Dictionary<K, T> innerCache = new Dictionary<K, T>();

        public K[] Keys
        {
            get
            {
                cacheLock.EnterReadLock();
                try
                {
                    return innerCache.Keys.ToArray();
                }
                finally
                {
                    cacheLock.ExitReadLock();
                }
            }
        }

        public bool TryRead(K key, out T value)
        {
            cacheLock.EnterReadLock();
            try
            {
                return innerCache.TryGetValue(key, out value);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public T Read(K key)
        {
            cacheLock.EnterReadLock();
            try
            {
                return innerCache[key];
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Add(K key, T value)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Add(key, value);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public bool AddWithTimeout(K key, T value, int timeout)
        {
            if (cacheLock.TryEnterWriteLock(timeout))
            {
                try
                {
                    innerCache.Add(key, value);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public AddOrUpdateStatus AddOrUpdate(K key, T value)
        {
            cacheLock.EnterUpgradeableReadLock();
            try
            {
                T result = default(T);
                if (innerCache.TryGetValue(key, out result))
                {
                    if (result.Equals(value))
                    {
                        return AddOrUpdateStatus.Unchanged;
                    }
                    else
                    {
                        cacheLock.EnterWriteLock();
                        try
                        {
                            innerCache[key] = value;
                        }
                        finally
                        {
                            cacheLock.ExitWriteLock();
                        }
                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    cacheLock.EnterWriteLock();
                    try
                    {
                        innerCache.Add(key, value);
                    }
                    finally
                    {
                        cacheLock.ExitWriteLock();
                    }
                    return AddOrUpdateStatus.Added;
                }
            }
            finally
            {
                cacheLock.ExitUpgradeableReadLock();
            }
        }

        public void Delete(K key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Remove(key);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Clear();
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public enum AddOrUpdateStatus
        {
            Added,
            Updated,
            Unchanged
        };


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (innerCache != null)
                {
                    innerCache.Clear();
                    innerCache = null;
                }
                if (cacheLock != null)
                {
                    cacheLock.Dispose();
                    cacheLock = null;
                }
            }
        }

        #endregion

        public bool ContainsKey(K key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                return innerCache.ContainsKey(key);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}
