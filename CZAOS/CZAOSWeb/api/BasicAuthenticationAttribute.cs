using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KT.Extensions;

namespace CZAOSWeb.api
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            var url = actionContext.Request.RequestUri.AbsolutePath;
            const string TOKEN = "CZAOSToken";
            string token = this.GetHeaderValue(actionContext, TOKEN);

            if (token.IsNotNullOrEmpty())
            {
                string cookieToken = this.GetCookieToken();

                if (url.Equals("/api/security/logout"))
                {
                    base.OnActionExecuting(actionContext); //pass on to SecurityController-Logout
                }

                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated && cookieToken.Equals(token))
                {
                    base.OnActionExecuting(actionContext); //pass on to requested controller action
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else if (url.Equals("/api/security/login")) //authorization.IsNotNullOrEmpty() && 
            {
                base.OnActionExecuting(actionContext); //pass on to SecurityController-Login
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

        }

        private string GetHeaderValue(System.Web.Http.Controllers.HttpActionContext actionContext, string key)
        {
            try
            {
                string value = Convert.ToString(actionContext.Request.Headers.GetValues(key).First());
                return value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }        

        private string GetCookieToken()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                return authTicket.UserData;

            }
            else
                return string.Empty;
        }

    }
}