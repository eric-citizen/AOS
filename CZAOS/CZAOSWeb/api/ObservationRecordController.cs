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
    public class ObservationRecordController : CZAOSController<ObservationRecord> //ApiController
    {
        static readonly IObservationRepository<ObservationRecord> repository = new ObservationRecordRepository();

        public override HttpResponseMessage GetAll()
        {
            IEnumerable<ObservationRecord> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<ObservationRecord>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationRecord");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<ObservationRecord>(HttpStatusCode.OK, item);
            }

            return response;
        }     

        public override HttpResponseMessage Post(ObservationRecord item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationRecord");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {                
                item = repository.Add(item);
                response = Request.CreateResponse<ObservationRecord>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.ObservationRecordID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        [HttpPost]
        public HttpResponseMessage SubmitObservationRecords(IEnumerable<ObservationRecord> items)
        {
            HttpResponseMessage response;

            if (items == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationRecord");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    repository.Add(item);
                }
                response = Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;
        }

        public override HttpResponseMessage Put(ObservationRecord item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationRecord");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid ObservationRecord");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<ObservationRecord>(HttpStatusCode.OK, item);
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
            ObservationRecord item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationRecord");
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