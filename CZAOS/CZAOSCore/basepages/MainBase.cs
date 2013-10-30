using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;
using KT.Extensions;
using CZAOSCore.Enums;

namespace CZAOSCore.basepages
{
    public class MainBase : System.Web.UI.Page
    {        

        protected virtual void Page_Init(object sender, EventArgs e)
        {
             
            if (this.Title.IsNullOrEmpty())
            {
                this.Title = AppSettings.GetSetting("PageTitle");
            }
            else
            {
                this.Title = AppSettings.GetSetting("PageTitle") + " > " + this.Title;
            }
            
        }

        public static string VirtualRoot
        {
            get
            {
                if (HttpRuntime.AppDomainAppVirtualPath.EndsWith("/"))
                {
                    return HttpRuntime.AppDomainAppVirtualPath;

                }
                else
                {
                    return HttpRuntime.AppDomainAppVirtualPath + "/";

                }

            }
        }
        
        public string CurrentUserIP
        {
            get { return this.Request.UserHostAddress; }
        }

        public string PhysicalRoot
        {
            get { return Server.MapPath(VirtualRoot); }
        }

        public bool IsDebug
        {
            get
            {
                CompilationSection cs = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
                return cs.Debug;
            }
        }

        public bool IsMasterAdmin
        {
            get
            {
                return Roles.IsUserInRole(CoreUserTypeRoles.MasterAdmin.ToString());
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return Roles.IsUserInRole(CoreUserTypeRoles.Administrator.ToString());
            }
        }

        public bool IsEducationAdmin
        {
            get
            {
                return Roles.IsUserInRole(CoreUserTypeRoles.EducationAdmin.ToString());
            }
        }

        public bool IsObserver
        {
            get
            {
                return Roles.IsUserInRole(CoreUserTypeRoles.Observer.ToString());
            }
        }

        public int FormsTimeout
        {
            get
            {
                AuthenticationSection cs = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
                return Convert.ToInt32(cs.Forms.Timeout.TotalMinutes);
            }
        }

        public static string PageUrl
        {
            get { return SiteUrl + HttpContext.Current.Request.RawUrl; }
        }

        public static string SiteUrl
        {
            get { return HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority; }
        }

    }
}
