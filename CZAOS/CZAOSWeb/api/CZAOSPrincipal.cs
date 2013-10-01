using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;


namespace CZAOSWeb.api
{
    public class CZAOSPrincipal : IPrincipal
    {

        public CZAOSPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(this.UserName, role);
        }

        private string _userName = string.Empty;
        private string _token = string.Empty;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

    }

    public class ApiIdentity : IIdentity
    {
        public IPrincipal User { get; private set; }

        public ApiIdentity(IPrincipal user, string token)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
            _token = token;

        }

        public string Name
        {
            get { return this.User.Identity.Name; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        private string _token = string.Empty;

        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }        
    }

}