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
using System.Web;
using KT.Extensions;

namespace CZAOSWeb.api
{
    public class ObservationReportController : CZAOSController<ObservationReport> //ApiController
    {
        static readonly IObservationRepository<ObservationReport> repository = new ObservationReportRepository();

        public override HttpResponseMessage GetAll()
        {
            string observationId = HttpContext.Current.Request.Headers["observationId"];
            int id = 0;

            if (observationId.IsNumeric())
                id = observationId.ToInt32();

            IEnumerable<ObservationReport> records;

            if (id > 0)
            {
                records = repository.GetAll(id);
            }
            else
            {
                records = repository.GetAll();
            }

            //IEnumerable<ObservationReport> records = repository.GetAll();

            var response = Request.CreateResponse<IEnumerable<ObservationReport>>(HttpStatusCode.OK, records);
            return response;
        }

        public override HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response;
            var item = repository.Get(id);

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationReport");
                throw new HttpResponseException(response);
            }
            else
            {
                response = Request.CreateResponse<ObservationReport>(HttpStatusCode.OK, item);
            }

            return response;
        }

        //http://localhost:53637/api/schools/?district=Bexley
        //public HttpResponseMessage GetObservationReportsByRegionCode(string regionCode)
        //{
        //    IEnumerable<ObservationReport> items = repository.GetAll().Where(
        //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
        //
        //    var response = Request.CreateResponse<IEnumerable<ObservationReport>>(HttpStatusCode.OK, items);
        //    return response;
        //}        

        public override HttpResponseMessage Post(ObservationReport item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationReport");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                item.Deleted = false;
                item = repository.Add(item);
                response = Request.CreateResponse<ObservationReport>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.ReportID });
                response.Headers.Location = new Uri(uri);

            }
            else
            {
                response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());
            }

            return response;

        }

        public override HttpResponseMessage Put(ObservationReport item)
        {
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationReport");
                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                if (!repository.Update(item))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent("Invalid ObservationReport");
                    throw new HttpResponseException(response);
                }
                else
                {
                    response = Request.CreateResponse<ObservationReport>(HttpStatusCode.OK, item);
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
            ObservationReport item = repository.Get(id);
            HttpResponseMessage response;

            if (item == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Invalid ObservationReport");
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