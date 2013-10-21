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
            RequestProperties props = RequestProperties.Current;

            //this module just checks that the authenticated web app administrator has the required permissions for the requested url.
            //the url can be a page [admin/animals/default.aspx] or extensionless [admin/animals/]
            //we need to handle both scenarios:

            //user is not authenticated, so no need to check whether they have permissions or not - they don't have any!
            if (!context.User.Identity.IsAuthenticated)
            {
                return;
            }
            else //user authenticated - if request is neither for a page or an extensionless page we can bail. Also, only check requests for admin urls.
            {
                //not for an admin resourse - you can safely bail
                if (!context.Request.CurrentExecutionFilePath.StartsWith("/admin/"))
                {
                    return;
                }

                //not for an admin page, either aspx or extensionless - you can safely bail
                if (!props.IsPage && !props.IsExtensionless)
                {
                    return;
                }

                //if we reach here we should check for admin permissions beacuse:
                //  1) User is autenticated
                //  2) Request is for a page (or extensionless page) located within the /admin/ folder.               

                
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
                //return;
            }

            string url = props.StrippedVirtualPath;
            url = url.EnsureStartsWith("/");
            url = url.Replace("/admin/", "");

            AdminNavigation nav = AdminNavigationList.GetItem(url);

            if ((nav == null))
            {
                Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} but no permissions were found in the datastore.".FormatWith(context.User.Identity.Name, url));

                if (context.Session != null)
                    context.Session.AddToSession("nopermissions", true);

                context.Response.SafeRedirect("/admin/default.aspx");
                return;
            }
            else
            {
                if (nav.RoleList.Count == 0)
                {
                    Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} but has zero permissions.".FormatWith(context.User.Identity.Name, url));

                    if (context.Session != null)
                        context.Session.AddToSession("nopermissions", true);

                    context.Response.SafeRedirect("/admin/default.aspx");
                    return;
                }

                foreach (string role in nav.RoleList)
                {
                    if (Roles.IsUserInRole(role))
                    {
                        return;
                    }
                    else
                    {
                        Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} attempted to access {1} with no permissions.".FormatWith(context.User.Identity.Name, url));

                        if(context.Session != null)
                            context.Session.AddToSession("nopermissions", true);

                        context.Response.SafeRedirect("/admin/default.aspx");
                        break;
                    }
                }
                

            }

        }

        public void Dispose()
        {
            //nowt to dispose
        }

    }
}
