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

    public class SchoolController : CZAOSController<School> //ApiController
    {
        static readonly ICZAOSRepository<School> repository = new SchoolRepository();

        //[System.Web.Http.AcceptVerbs("GET")] ISchoolRepository
        //[System.Web.Http.HttpGet]
        public override HttpResponseMessage GetAll() //Schools()
        {
            IEnumerable<School> schools = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<School>>(HttpStatusCode.OK, schools);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var school = repository.Get(id); //products.FirstOrDefault((p) => p.Id == id);

            if (school == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content =  new StringContent("Invalid School");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<School>(HttpStatusCode.OK, school);                
            }

            return response; //school;
        }

        //http://localhost:53637/api/schools/?district=Bexley
        public HttpResponseMessage GetSchoolsByDistrict(string district)
        {
            IEnumerable<School> schools = repository.GetAll().Where(
                    s => string.Equals(s.DistrictName, district, StringComparison.OrdinalIgnoreCase));

            var response = Request.CreateResponse<IEnumerable<School>>(HttpStatusCode.OK, schools);
            return response;
        }

        public override HttpResponseMessage Post(School item)
        {           
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid School");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<School>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.SchoolID });
                response.Headers.Location = new Uri(uri);                

            }
            else 
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
            }

            return response;

        }

        public override HttpResponseMessage Put(School school)
        {
            HttpResponseMessage response;

            if (school == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid School");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(school))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid School");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<School>(HttpStatusCode.OK, school);                    
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
            School item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid School");
                throw new HttpResponseException(response);
            }
            else
            {                
                repository.Remove(id);
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return response;
        }

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }

}