using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web.Http;
using CZAOSCore;
using CZAOSCore.Logging;
using CZAOSWeb.api;

namespace CZAOSWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            bool apiSecure = AppSettings.GetSetting<bool>("APISecure");

            if (apiSecure)
            {
                RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            }
            
            //MapRoutes(RouteTable.Routes);

            //http://encosia.com/rest-vs-rpc-in-asp-net-web-api-who-cares-it-does-both/

            RouteTable.Routes.MapHttpRoute(
                name: "UserGetAll",
                routeTemplate: "api/User/GetAll",
                defaults: new { controller = "User", action = "GetAll", id = RouteParameter.Optional }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

            RouteTable.Routes.MapHttpRoute(
               name: "ActionApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            //RouteTable.Routes.MapHttpRoute("DefaultApiWithId", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            //RouteTable.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");
            //RouteTable.Routes.MapHttpRoute("DefaultApiGet", "api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint("Get") });

            //RouteTable.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = "get", id = RouteParameter.Optional }
            //);

            Logger.Initialize();
            Logger.Log(LogTarget.Root, MessageLevel.Info, "=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*");
            Logger.Log(LogTarget.Root, MessageLevel.Info, "=*=*=*=*=*=* Application_Start =*=*=*=*=*=*");
            Logger.Log(LogTarget.Root, MessageLevel.Info, "=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*");

        }

        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new CZAOSWeb.api.BasicAuthenticationAttribute());
        }

        //private void MapRoutes(RouteCollection routes)
        //{
        //    //routes.MapHttpRoute("DefaultApiWithId", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
        //    //routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");
        //    routes.MapHttpRoute("DefaultApiGet", "api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint("Get") });
        //    routes.MapHttpRoute("DefaultApiPost", "api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint("Post") });
        //}

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger.LogError(ErrorLevel.Error, HttpContext.Current.Server.GetLastError(), true);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

        //    if (authCookie != null)
        //    {
        //        if (HttpContext.Current.User == null)
        //        {
        //            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //            //JavaScriptSerializer serializer = new JavaScriptSerializer();
        //            //CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

        //            CZAOSPrincipal newUser = new CZAOSPrincipal(authTicket.Name);
        //            newUser.Token = authTicket.UserData;
        //            HttpContext.Current.User = newUser;
        //        }
        //        else
        //        {
        //            if(HttpContext.Current.User is CZAOSPrincipal)
        //            {
        //                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //                CZAOSPrincipal newUser = (CZAOSPrincipal)HttpContext.Current.User;
        //                newUser.Token = authTicket.UserData;
        //                HttpContext.Current.User = newUser;
        //            }
                   
        //        }
                

        //    }
        //}
    }
}