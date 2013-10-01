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

    public class BehaviorController : CZAOSController<Behavior> //ApiController
    {
        static readonly ICZAOSRepository<Behavior> repository = new BehaviorRepository();
       
        public override HttpResponseMessage GetAll() 
        {
            IEnumerable<Behavior> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<Behavior>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);  

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Behavior");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<Behavior>(HttpStatusCode.OK, item);                
            }

            return response;  
        }

        //http://localhost:53637/api/schools/?district=Bexley
        public HttpResponseMessage GetBehaviorByCategory(string categoryCode)
        {
            IEnumerable<Behavior> items = repository.GetAll().Where(
                    s => string.Equals(s.BehaviorCategoryCode, categoryCode, StringComparison.OrdinalIgnoreCase));

            var response = Request.CreateResponse<IEnumerable<Behavior>>(HttpStatusCode.OK, items);
            return response;
        }

        public override HttpResponseMessage Post(Behavior item)
        {           
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Behavior");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<Behavior>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.BehaviorID });
                response.Headers.Location = new Uri(uri);                

            }
            else 
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
            }

            return response;

        }

        public override HttpResponseMessage Put(Behavior item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Behavior");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid Behavior");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<Behavior>(HttpStatusCode.OK, item);                    
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
            Behavior item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Behavior");
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