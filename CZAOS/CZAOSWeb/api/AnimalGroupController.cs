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


    public class AnimalGroupController : CZAOSController<AnimalGroup> //ApiController
    {
        static readonly IObservationRepository<AnimalGroup> repository = new AnimalGroupRepository();

        public override HttpResponseMessage GetAll()
        {
            string observationId = HttpContext.Current.Request.Headers["observationId"];
            int id = 0;

            if (observationId.IsNumeric())
                id = observationId.ToInt32();

            IEnumerable<AnimalGroup> records;

            if (id == 0)
                records = repository.GetAll();
            else
                records = repository.GetAll(id);

            //IEnumerable<AnimalGroup> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<AnimalGroup>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalGroup");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<AnimalGroup>(HttpStatusCode.OK, item);
            }

            return response;
        }
  

        public override HttpResponseMessage Post(AnimalGroup item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalGroup");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Deleted = false;
                item = repository.Add(item);
                response = Request.CreateResponse<AnimalGroup>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.AnimalGroupID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(AnimalGroup item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalGroup");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid AnimalGroup");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<AnimalGroup>(HttpStatusCode.OK, item);
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
            AnimalGroup item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalGroup");
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