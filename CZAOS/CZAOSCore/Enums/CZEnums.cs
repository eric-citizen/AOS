using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CZAOSCore.Enums
{
    public enum CoreUserTypeRoles
    {
        MasterAdmin,
        Administrator,
        EducationAdmin,
        Observer
    }

    public enum CoreUserTypes
    {
        Professional,
        Education,
        Amateur
    }

    public class Helpers
    {
        public static List<string> GetCoreUserTypes()
        {
            return Enum.GetNames(typeof(CoreUserTypes)).ToList();
        }

        public static List<string> GetCoreUserTypeRoles()
        {
            return Enum.GetNames(typeof(CoreUserTypeRoles)).ToList();
        }
    }

}
