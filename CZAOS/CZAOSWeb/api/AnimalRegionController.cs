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

//http://blogs.telerik.com/aspnet-ajax/posts/12-04-26/take-a-walk-on-the-client-side-with-webapi-and-webforms.aspx

namespace CZAOSWeb.api
{

    public class AnimalRegionController : ApiController
    {
        static readonly AnimalRegionRepository repository = new AnimalRegionRepository();
       
        public HttpResponseMessage GetAll() 
        {
            IEnumerable<AnimalRegion> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<AnimalRegion>>(HttpStatusCode.OK, records);
            return response;
        }

        public HttpResponseMessage Get(string id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);  

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalRegion");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<AnimalRegion>(HttpStatusCode.OK, item);                
            }

            return response;  
        }

        public HttpResponseMessage Post(AnimalRegion item)
        {           
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalRegion");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<AnimalRegion>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.AnimalRegionCode });
                response.Headers.Location = new Uri(uri);                

            }
            else 
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
            }

            return response;

        }

        public HttpResponseMessage Put(AnimalRegion item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalRegion");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid AnimalRegion");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<AnimalRegion>(HttpStatusCode.OK, item);                    
                }
            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());   
            }        

            return response;
        }

        public HttpResponseMessage Delete(string id)
        {
            AnimalRegion item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid AnimalRegion");
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