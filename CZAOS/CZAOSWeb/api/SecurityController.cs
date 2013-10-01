using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Threading;
using System.Security.Principal;
using System.Web.Security;
using System.Text;
using KT.Extensions;
using CZAOSCore.Logging;
using CZAOSCore.Enums;

//http://blogs.telerik.com/aspnet-ajax/posts/12-04-26/take-a-walk-on-the-client-side-with-webapi-and-webforms.aspx
//http://stevescodingblog.co.uk/basic-authentication-with-asp-net-webapi/
//http://www.c-sharpcorner.com/blogs/2918/how-to-set-a-request-header-in-a-jquery-ajax-call.aspx
//http://andyck1.blogspot.com/2012/10/webapi-and-subscriber-authentication.html
//https://github.com/rmbrunet/WebApiAntiForgeryPOC/blob/master/WebApiAntiForgeryTokenPrototype/Filters/ValidateAntiForgeryTokenAttribute.cs
//https://github.com/Mikaelo/WebAPI_message_handlers/blob/master/WebAPI/TokenValidationAttribute.cs

namespace CZAOSWeb.api
{   

    public class SecurityController : ApiController
    {        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Login()
        {
            HttpResponseMessage response;
            string creds = HttpContext.Current.Request.Headers["Authorization"];

            if (creds.IsNullOrEmpty())
            {
                response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string decodedText = creds.DecodeString();
                string username = decodedText.Substring(0, decodedText.IndexOf(":"));
                string password = decodedText.Substring(decodedText.IndexOf(":") + 1);

                if (this.ValidateLogin(username, password))
                {
                    System.Guid gtoken = System.Guid.NewGuid();


                    //Cookies
                    FormsAuthentication.Initialize();
                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, false);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, gtoken.ToString());

                    authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                    HttpContext.Current.Response.Cookies.Add(authCookie);

                    //Principal
                    CZAOSPrincipal principal = new CZAOSPrincipal(username);
                    //HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] { });

                    principal.Token = gtoken.ToString();

                    this.SetPrincipal(new GenericPrincipal(new ApiIdentity(principal, gtoken.ToString()), new string[] { }));
                   
                    response = Request.CreateResponse<string>(HttpStatusCode.OK, principal.Token);

                    Logger.Log(LogTarget.Security, MessageLevel.Info, "User {0} logged in to the API successfully.".FormatWith(username));
 
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    response.Content = new StringContent("Invalid API Login attempt");
                    Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} failed to log in to the API.".FormatWith(username));
                }

            }            

            return response;
        }

        //[System.Web.Http.HttpPost]
        public HttpResponseMessage Logout()
        {
            HttpResponseMessage response;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (authCookie != null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
                authCookie.Expires = System.DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(authCookie);
                FormsAuthentication.SignOut();
            }

            response = Request.CreateResponse(HttpStatusCode.OK);
            
            return response;

        }
        
        //private CZAOSPrincipal AuthorizedUser
        //{
        //    get
        //    {
        //        return HttpContext.Current.User as CZAOSPrincipal;
        //    }

        //}

        private bool ValidateLogin(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }    
    }
}