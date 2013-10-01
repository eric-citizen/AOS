using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;


namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class ChangeLogList  : BaseList<ChangeLog>
	{
		#region CONSTRUCTION/LOAD

		 public ChangeLogList()
        {
        }

        public ChangeLogList(List<ChangeLog> items)
        {
            base.AddRange(items);
        }

        public void Load()
        {
            this.AddRange(GetItemCollection());
        }
		#endregion

		#region ADMIN METHODS

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ChangeLogList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("ChangeDate DESC, UserDisplayName");
			return new ChangeLogList(ChangeLogProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ChangeLogList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ChangeLogList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ChangeLogList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ChangeLog GetItem(int id)
		{
			return ChangeLogProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ChangeLogProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ChangeLogProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static ChangeLog AddItem(ChangeLog item)
        {          
            return ChangeLogProvider.Instance().AddItem(item);
        }		

		#endregion
		
	}   
	
    //public class ChangeLogRepository : ICZAOSRepository<ChangeLog>
    //{
    //    public IEnumerable<ChangeLog> GetAll()
    //    {
    //        List<ChangeLog> records = ChangeLogList.GetActive();
    //        return records;
    //    }

    //    public ChangeLog Get(int id)
    //    {
    //        return ChangeLogList.Get(id);
    //    }

    //    public ChangeLog Add(ChangeLog item)
    //    {
    //        return ChangeLogList.AddItem(item);
    //    }

    //    public void Remove(int id)
    //    {
    //        ChangeLogList.DeleteItem(id);
    //    }

    //    public bool Update(ChangeLog item)
    //    {
    //        ChangeLogList.UpdateItem(item);
    //        return true;
    //    }
    //}

    //public class ChangeLogController : CZAOSController<ChangeLog> //ApiController
    //{
    //    static readonly ICZAOSRepository<ChangeLog> repository = new ChangeLogRepository();
       
    //    public override HttpResponseMessage GetAll() 
    //    {
    //        IEnumerable<ChangeLog> records = repository.GetAll();
    //        var response = Request.CreateResponse<IEnumerable<ChangeLog>>(HttpStatusCode.OK, records);
    //        return response;
    //    }

    //    public override HttpResponseMessage Get(int id)
    //    {
    //        HttpResponseMessage response;
    //        var item = repository.Get(id);  

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content =  new StringContent("Invalid ChangeLog");
    //            throw new HttpResponseException(response);
    //        }
    //        else
    //        {
    //            response = Request.CreateResponse<ChangeLog>(HttpStatusCode.OK, item);                
    //        }

    //        return response;  
    //    }

    //    //http://localhost:53637/api/schools/?district=Bexley
    //    //public HttpResponseMessage GetChangeLogsByRegionCode(string regionCode)
    //    //{
    //    //    IEnumerable<ChangeLog> items = repository.GetAll().Where(
    //    //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
    //    //
    //    //    var response = Request.CreateResponse<IEnumerable<ChangeLog>>(HttpStatusCode.OK, items);
    //    //    return response;
    //    //}        

    //    public override HttpResponseMessage Post(ChangeLog item)
    //    {           
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid ChangeLog");
    //            throw new HttpResponseException(response);
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            item.Active = true;
    //            item = repository.Add(item);
    //            response = Request.CreateResponse<ChangeLog>(HttpStatusCode.Created, item);

    //            string uri = Url.Link("DefaultApi", new { id = item.ChangeLogID });
    //            response.Headers.Location = new Uri(uri);                

    //        }
    //        else 
    //        {
    //            response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
    //        }

    //        return response;

    //    }

    //    public override HttpResponseMessage Put(ChangeLog item)
    //    {
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid ChangeLog");
    //            throw new HttpResponseException(response);
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            if (!repository.Update(item))
    //            {
    //                response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //                response.Content = new StringContent("Invalid ChangeLog");
    //                throw new HttpResponseException(response);
    //            }
    //            else
    //            {
    //                response = Request.CreateResponse<ChangeLog>(HttpStatusCode.OK, item);                    
    //            }
    //        }
    //        else
    //        {
    //            response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());   
    //        }        

    //        return response;
    //    }

    //    public override HttpResponseMessage Delete(int id)
    //    {
    //        ChangeLog item = repository.Get(id);
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid ChangeLog");
    //            throw new HttpResponseException(response);
    //        }
    //        else
    //        {                
    //            repository.Remove(id);
    //            response = new HttpResponseMessage(HttpStatusCode.NoContent);
    //        }

    //        return response;
    //    }
       
    //}
	
}



