using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore;
using CZAOSCore.Logging;

namespace CZAOSWeb.Security
{
    public class AdminPermissionsModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext current = app.Context;
            this.CheckPermissions(current);
        }

        private void CheckPermissions(HttpContext context)
        {
            if (context.Request.CurrentExecutionFilePath.Contains("api/"))
                return;

            //only check pages in admin module
            RequestProperties props = RequestProperties.Current;

            if (!context.Request.CurrentExecutionFilePath.StartsWith("/admin/") || !props.IsPage)
            {
                if (context.Request.CurrentExecutionFilePath.StartsWith("/teacher/")) 
                {
                    var login = context.Request.QueryString["login"]; ;
                    var pass = context.Request.QueryString["pass"]; ;
                    var id = context.Request.QueryString["observationID"];
                    var obs = ObservationList.Get(id.ToInt32());
                    if (obs.TeacherLogin == login && obs.TeacherPass == pass)
                        return;
                }
                if (!props.IsExtensionless)
                {
                    return;
                }
                else
                {
                    if (!props.IsPage)
                    {
                        return;
                    }
                }
            }

            if (!context.User.Identity.IsAuthenticated)
            {
                return;
            }
            else
            {
                //page.ViewStateUserKey = page.User.Identity.Name;
                if (context.Handler is System.Web.UI.Page)
                {
                    System.Web.UI.Page @this = (System.Web.UI.Page)context.Handler;
                    @this.ViewStateUserKey = @this.User.Identity.Name;
                }
                

            }

            //always allow acces to admin home if (props.RawUrl.Equals("/admin/default.aspx") || props.RawUrl.Equals("/admin/"))
            if (props.StrippedVirtualPath.Equals("admin"))
            {
                return;
            }

            //always allow master admins
            string rolename = CZAOSCore.Enums.CoreUserTypeRoles.MasterAdmin.ToString();            
            if (Roles.IsUserInRole(rolename))
            {
                return;
            }

            string url = props.StrippedVirtualPath;
            url = url.EnsureStartsWith("/");
            url = url.Replace("/admin/", "");

            AdminNavigation nav = AdminNavigationList.GetItem(url);

            if ((nav == null))
            {
                Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} but no permissions were found in the datastore.".FormatWith(context.User.Identity.Name, url));
                context.Session.AddToSession("nopermissions", true);
                context.Response.SafeRedirect("/admin/observation/default.aspx");
                return;
            }
            else
            {
                if (nav.RoleList.Count == 0)
                {
                    Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} but has zero permissions.".FormatWith(context.User.Identity.Name, url));
                    context.Session.AddToSession("nopermissions", true);
                    context.Response.SafeRedirect("/admin/observation/default.aspx");
                    return;
                }
                foreach (string role in nav.RoleList)
                {
                    if (Roles.IsUserInRole(role))
                    {
                        return;
                    }
                    
                }
                Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} with no permissions.".FormatWith(context.User.Identity.Name, url));
                context.Session.AddToSession("nopermissions", true);
                context.Response.SafeRedirect("/admin/observation/default.aspx");

            }

        }

        public void Dispose()
        {
            //nowt to dispose
        }

    }
}
