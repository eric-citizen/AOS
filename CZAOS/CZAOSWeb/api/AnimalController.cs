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

    public class AnimalController : CZAOSController<Animal> //ApiController
    {
        static readonly ICZAOSRepository<Animal> repository = new AnimalRepository();
       
        public override HttpResponseMessage GetAll() 
        {
            IEnumerable<Animal> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<Animal>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);  

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content =  new StringContent("Invalid Animal");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<Animal>(HttpStatusCode.OK, item);                
            }

            return response;  
        }

        //http://localhost:53637/api/schools/?district=Bexley
        public HttpResponseMessage GetAnimalsByRegionCode(string regionCode)
        {
            IEnumerable<Animal> items = repository.GetAll().Where(
                    s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));

            var response = Request.CreateResponse<IEnumerable<Animal>>(HttpStatusCode.OK, items);
            return response;
        }

        public HttpResponseMessage GetAnimalsByZooID(string zooId)
        {
            IEnumerable<Animal> items = repository.GetAll().Where(
                   s => string.Equals(s.ZooID, zooId, StringComparison.OrdinalIgnoreCase));

            var response = Request.CreateResponse<IEnumerable<Animal>>(HttpStatusCode.OK, items);
            return response;
        }

        public override HttpResponseMessage Post(Animal item)
        {           
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Animal");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<Animal>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.AnimalID });
                response.Headers.Location = new Uri(uri);                

            }
            else 
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
            }

            return response;

        }

        public override HttpResponseMessage Put(Animal item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Animal");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid Animal");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<Animal>(HttpStatusCode.OK, item);                    
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
            Animal item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Animal");
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