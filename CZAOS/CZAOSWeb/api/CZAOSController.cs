using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CZAOSWeb.api
{
    //[Authorize(Roles = "Administrators")]
    //[BasicAuthentication]
    public abstract class CZAOSController<T> : ApiController
    {

        #region  Abstract Methods   

        public abstract HttpResponseMessage GetAll();
        public abstract HttpResponseMessage Get(int id);

        //[Authorize(Roles = "Administrators")]
        public abstract HttpResponseMessage Delete(int id);
        public abstract HttpResponseMessage Post(T item);
        public abstract HttpResponseMessage Put(T item);

        #endregion

        
    }
}