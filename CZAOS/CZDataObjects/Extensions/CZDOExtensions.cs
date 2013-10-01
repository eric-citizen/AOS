using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZDataObjects.Extensions
{
    public static class CZDOExtensions
    {
        public static string ToYesNoString(this bool value)
        {
            return value ? "Y" : "N";
        }

        public static bool FromYesNoString(this string value)
        {
            return value == "Y" ? true : false;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
 