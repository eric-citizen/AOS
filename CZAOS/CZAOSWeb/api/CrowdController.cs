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
using KT.Extensions;

namespace CZAOSWeb.api
{
    public class CrowdController : CZAOSController<Crowd> //ApiController
    {
        static readonly ICZAOSRepository<Crowd> repository = new CrowdRepository();

        public override HttpResponseMessage GetAll()
        {
            IEnumerable<Crowd> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<Crowd>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Crowd");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<Crowd>(HttpStatusCode.OK, item);
            }

            return response;
        }

        //http://localhost:53637/api/schools/?district=Bexley
        //public HttpResponseMessage GetCrowdsByRegionCode(string regionCode)
        //{
        //    IEnumerable<Crowd> items = repository.GetAll().Where(
        //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
        //
        //    var response = Request.CreateResponse<IEnumerable<Crowd>>(HttpStatusCode.OK, items);
        //    return response;
        //}        

        public override HttpResponseMessage Post(Crowd item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Crowd");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<Crowd>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.CrowdID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(Crowd item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Crowd");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid Crowd");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<Crowd>(HttpStatusCode.OK, item);
                }
            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;
        }

        public override HttpResponseMessage Delete(int id)
        {
            Crowd item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Crowd");
                throw new HttpResponseException(response);
            }
            else
            {
                repository.Remove(id);
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return response;
        }

    }
}