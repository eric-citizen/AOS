using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZAOSCore
{
    public interface ICacheItem
    {
        DateTime? AbsoluteExpiration { get; set; }        
        string Group { get; set; }
        string Key { get; set; }
        TimeSpan? SlidingExpiration { get; set; }
        byte[] Value { get; set; }
    }
}