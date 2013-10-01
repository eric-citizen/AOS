using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZAOSCore
{
    public class InternalCacheItem : ICacheItem
    {
        public DateTime? AbsoluteExpiration
        {
            get;
            set;
        }       

        public string Group
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public TimeSpan? SlidingExpiration
        {
            get;
            set;
        }

        public byte[] Value
        {
            get;
            set;
        }
    }
}