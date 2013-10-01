using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CZBizObjects;
using CZBizObjects.Repository;
using CZDataObjects;
using System.Text;

namespace CZAOSWeb.api
{
    public class UserController : ApiController
    {
        static readonly UserRepository repository = new UserRepository();

        public HttpResponseMessage GetAll()
        {            
            IEnumerable<CZUser> users = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<CZUser>>(HttpStatusCode.OK, users);
            return response;            
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;

            if (HttpContext.Current.User != null)
            {
                string username = HttpContext.Current.User.Identity.Name;
                var user = repository.Get(username);

                if (user == null)
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid User");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<CZUser>(HttpStatusCode.OK, user);
                }

            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid User");
                throw new HttpResponseException(response);
            }

            return response;  
        }

        public HttpResponseMessage Get(string email)
        {
            HttpResponseMessage response;

            var user = repository.GetByEmail(email);

            if (user == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid User");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<CZUser>(HttpStatusCode.OK, user);
            }

            return response;
        }              

        public HttpResponseMessage Post(CZUser item)
        {           
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid CZUser");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<CZUser>(HttpStatusCode.Created, item);               

            }
            else 
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
            }

            return response;

        }

        public HttpResponseMessage Put(CZUser user)
        {
            HttpResponseMessage response;

            if (user == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid CZUser");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(user))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid CZUser");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<CZUser>(HttpStatusCode.OK, user);                    
                }
            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());   
            }        

            return response;
        }        

    }

}