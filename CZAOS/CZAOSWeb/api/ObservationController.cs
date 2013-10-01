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
    public class ObservationController : CZAOSController<Observation> //ApiController
    {
        static readonly ICZAOSRepository<Observation> repository = new ObservationRepository();

        public override HttpResponseMessage GetAll()
        {
            IEnumerable<Observation> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<Observation>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observation");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<Observation>(HttpStatusCode.OK, item);
            }

            return response;
        } 

        public override HttpResponseMessage Post(Observation item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observation");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Deleted = true;
                item = repository.Add(item);
                response = Request.CreateResponse<Observation>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.ObservationID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(Observation item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observation");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid Observation");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<Observation>(HttpStatusCode.OK, item);
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
            Observation item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Observation");
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