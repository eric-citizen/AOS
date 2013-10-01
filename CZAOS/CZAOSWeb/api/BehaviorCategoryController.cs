﻿using System;
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
    public class BehaviorCategoryController : CZAOSController<BehaviorCategory> //ApiController
    {
        static readonly ICZAOSRepository<BehaviorCategory> repository = new BehaviorCategoryRepository();

        public override HttpResponseMessage GetAll()
        {
            IEnumerable<BehaviorCategory> records = repository.GetAll();
            var response = Request.CreateResponse<IEnumerable<BehaviorCategory>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid BehaviorCategory");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<BehaviorCategory>(HttpStatusCode.OK, item);
            }

            return response;
        }

        //http://localhost:53637/api/schools/?district=Bexley
        //public HttpResponseMessage GetBehaviorCategorysByRegionCode(string regionCode)
        //{
        //    IEnumerable<BehaviorCategory> items = repository.GetAll().Where(
        //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
        //
        //    var response = Request.CreateResponse<IEnumerable<BehaviorCategory>>(HttpStatusCode.OK, items);
        //    return response;
        //}        

        public override HttpResponseMessage Post(BehaviorCategory item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid BehaviorCategory");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Active = true;
                item = repository.Add(item);
                response = Request.CreateResponse<BehaviorCategory>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.BvrCatID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(BehaviorCategory item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid BehaviorCategory");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid BehaviorCategory");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<BehaviorCategory>(HttpStatusCode.OK, item);
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
            BehaviorCategory item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid BehaviorCategory");
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