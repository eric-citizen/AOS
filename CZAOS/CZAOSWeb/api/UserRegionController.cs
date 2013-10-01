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
    public class UserRegionController : CZAOSController<UserRegion> //ApiController
    {
        static readonly ICZAOSRepository<UserRegion> repository = new UserRegionRepository();

        public override HttpResponseMessage GetAll()
        {
            IEnumerable<UserRegion> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<UserRegion>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid UserRegion");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<UserRegion>(HttpStatusCode.OK, item);
            }

            return response;
        }        

        public HttpResponseMessage GetByUser(string username)
        {
            HttpResponseMessage response;           

            if (username.IsNullOrEmpty())
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid Username");
                throw new HttpResponseException(response);
            }
            else
            {
                IEnumerable<UserRegion> users = repository.GetAll().Where(
                    s => string.Equals(s.Username, username, StringComparison.OrdinalIgnoreCase));

                response = Request.CreateResponse<IEnumerable<UserRegion>>(HttpStatusCode.OK, users);                
            }

            return response;
        }

        public override HttpResponseMessage Post(UserRegion item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid UserRegion");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<UserRegion>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.UserRegionID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(UserRegion item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid UserRegion");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid UserRegion");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<UserRegion>(HttpStatusCode.OK, item);
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
            UserRegion item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid UserRegion");
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