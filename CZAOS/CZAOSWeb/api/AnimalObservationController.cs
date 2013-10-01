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
    public class AnimalObservationController : CZAOSController<AnimalObservation> //ApiControllerx
    {
        static readonly AnimalObservationRepository repository = new AnimalObservationRepository();

        public override HttpResponseMessage GetAll()
        {
            string observationId = HttpContext.Current.Request.Headers["observationId"];
            int id = 0;

            if (observationId.IsNumeric())
                id = observationId.ToInt32();

            IEnumerable<AnimalObservation> records;

            if (id > 0)
            {
                records = repository.GetAll(id);
            }
            else
            {
                records = repository.GetAll();
            }

            //IEnumerable<AnimalObservation> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<AnimalObservation>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalObservation");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<AnimalObservation>(HttpStatusCode.OK, item);
            }

            return response;
        }

        public override HttpResponseMessage Post(AnimalObservation item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalObservation");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {                
                item = repository.Add(item);
                response = Request.CreateResponse<AnimalObservation>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.AnimalObservationID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(AnimalObservation item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalObservation");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid AnimalObservation");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<AnimalObservation>(HttpStatusCode.OK, item);
                }
            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;
        }

        public override HttpResponseMessage Delete(int observationId)
        {            
            HttpResponseMessage response;

            repository.Remove(observationId);
            response = new HttpResponseMessage(HttpStatusCode.NoContent);

            return response;
        }

    }

}