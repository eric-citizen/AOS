using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KT.Extensions;

namespace CZAOSCore
{
    public class AppSettings
    {
        public static string GetSetting(string key)
        {
            string strValue = CacheManager.GetFromCache<string>(key);

            if (strValue.IsNullOrEmpty())
            {
                strValue = System.Configuration.ConfigurationManager.AppSettings.Get(key);
                CacheManager.CacheItem(strValue, key);
            }

            return strValue;            
        }

        public static T GetSetting<T>(string key)
        {
            string strValue = CacheManager.GetFromCache<string>(key);

            if (strValue.IsNullOrEmpty())
            {
                strValue = System.Configuration.ConfigurationManager.AppSettings.Get(key);
                CacheManager.CacheItem(strValue, key);
            }

            return (T)Convert.ChangeType(strValue, typeof(T));

        }
    }
}



