using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CZBizObjects;
using CZBizObjects.Repository;
using CZDataObjects;
using System.Text;

namespace CZAOSWeb.api
{
    public class SysCodeController : ApiController
    {
        static readonly SysCodeRepository repository = new SysCodeRepository();

        public HttpResponseMessage GetAll()
        {
            IEnumerable<SysCode> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<SysCode>>(HttpStatusCode.OK, records);
            return response;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid SysCode");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<SysCode>(HttpStatusCode.OK, item);
            }

            return response;
        }

        public HttpResponseMessage Get(string key)
        {
            HttpResponseMessage response;
            var item = repository.Get(key);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid SysCode");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<SysCode>(HttpStatusCode.OK, item);
            }

            return response;
        }
    }
}