using System;
using System.Web;
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

    public class ObserverController : CZAOSController<Observer> //ApiController
    {
        static readonly IObservationRepository<Observer> repository = new ObserverRepository();

        public override HttpResponseMessage GetAll()
        {
            string observationId = HttpContext.Current.Request.Headers["observationId"];
            int id = 0;

            if (observationId.IsNumeric())
                id = observationId.ToInt32();

            IEnumerable<Observer> records;

            if(id == 0)
                records = repository.GetAll();
            else
                records = repository.GetAll(id);

            var response = Request.CreateResponse<IEnumerable<Observer>>(HttpStatusCode.OK, records);
            return response;
        }        

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observer");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<Observer>(HttpStatusCode.OK, item);
            }

            return response;
        }

        //http://localhost:53637/api/schools/?district=Bexley
        //public HttpResponseMessage GetObserversByRegionCode(string regionCode)
        //{
        //    IEnumerable<Observer> items = repository.GetAll().Where(
        //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
        //
        //    var response = Request.CreateResponse<IEnumerable<Observer>>(HttpStatusCode.OK, items);
        //    return response;
        //}        

        public override HttpResponseMessage Post(Observer item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observer");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Deleted = false;
                item.Locked = false;

                item = repository.Add(item);
                response = Request.CreateResponse<Observer>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.ObserverID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(Observer item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observer");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid Observer");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<Observer>(HttpStatusCode.OK, item);
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
            Observer item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observer");
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