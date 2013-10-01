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
	[DataObject()] public class EmailTemplateList  : BaseList<EmailTemplate>
	{
		#region CONSTRUCTION/LOAD

		 public EmailTemplateList()
        {
        }

        public EmailTemplateList(List<EmailTemplate> items)
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
		public static EmailTemplateList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("Active");
			return new EmailTemplateList(EmailTemplateProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmailTemplateList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmailTemplateList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmailTemplateList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmailTemplate GetItem(int id)
		{
			return EmailTemplateProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return EmailTemplateProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return EmailTemplateProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static EmailTemplate AddItem(EmailTemplate item)
        {         
			EmailTemplate nw = EmailTemplateProvider.Instance().AddItem(item);

            Audit(nw, ChangeLog.LogChangeType.create, nw.ID);

            RemoveCacheList();

            return nw;                   
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(EmailTemplate item)
        {
            EmailTemplate original = Get(item.ID);

            AuditUpdate(original, item, item.ID);

            EmailTemplateProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(EmailTemplate item)
        {
            DeleteItem(item.ID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {    
			Audit(typeof(EmailTemplate), ChangeLog.LogChangeType.delete, id);
            EmailTemplateProvider.Instance().DeleteItem(id);
            RemoveCacheList();           		
            
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTemplateList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (active)
            {
                filter = "Active = 1";
            }
            else
            {
                filter = "Active = 0";
            }

            return GetItemCollection(0, 0, "Active", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTemplateList GetActive()
        {
            List<EmailTemplate> items = GetCacheList();

            if (items == null)
            {
                items = EmailTemplateList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new EmailTemplateList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTemplate Get(int id)
        {
            EmailTemplateList items = GetActive();
            EmailTemplate item = items.FirstOrDefault(s => s.ID == id);

            return item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTemplate Get(string key)
        {
            EmailTemplateList items = GetActive();
            EmailTemplate item = items.FirstOrDefault(s => s.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); 

            return item;
        }

        #endregion     
		
	}   
	
    //public class EmailTemplateRepository : ICZAOSRepository<EmailTemplate>
    //{
    //    public IEnumerable<EmailTemplate> GetAll()
    //    {
    //        List<EmailTemplate> records = EmailTemplateList.GetActive();
    //        return records;
    //    }

    //    public EmailTemplate Get(int id)
    //    {
    //        return EmailTemplateList.Get(id);
    //    }

    //    public EmailTemplate Add(EmailTemplate item)
    //    {
    //        return EmailTemplateList.AddItem(item);
    //    }

    //    public void Remove(int id)
    //    {
    //        EmailTemplateList.DeleteItem(id);
    //    }

    //    public bool Update(EmailTemplate item)
    //    {
    //        EmailTemplateList.UpdateItem(item);
    //        return true;
    //    }
    //}

    //public class EmailTemplateController : CZAOSController<EmailTemplate> //ApiController
    //{
    //    static readonly ICZAOSRepository<EmailTemplate> repository = new EmailTemplateRepository();
       
    //    public override HttpResponseMessage GetAll() 
    //    {
    //        IEnumerable<EmailTemplate> records = repository.GetAll();
    //        var response = Request.CreateResponse<IEnumerable<EmailTemplate>>(HttpStatusCode.OK, records);
    //        return response;
    //    }

    //    public override HttpResponseMessage Get(int id)
    //    {
    //        HttpResponseMessage response;
    //        var item = repository.Get(id);  

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content =  new StringContent("Invalid EmailTemplate");
    //            throw new HttpResponseException(response);
    //        }
    //        else
    //        {
    //            response = Request.CreateResponse<EmailTemplate>(HttpStatusCode.OK, item);                
    //        }

    //        return response;  
    //    }

    //    //http://localhost:53637/api/schools/?district=Bexley
    //    //public HttpResponseMessage GetEmailTemplatesByRegionCode(string regionCode)
    //    //{
    //    //    IEnumerable<EmailTemplate> items = repository.GetAll().Where(
    //    //            s => string.Equals(s.AnimalRegionCode, regionCode, StringComparison.OrdinalIgnoreCase));
    //    //
    //    //    var response = Request.CreateResponse<IEnumerable<EmailTemplate>>(HttpStatusCode.OK, items);
    //    //    return response;
    //    //}        

    //    public override HttpResponseMessage Post(EmailTemplate item)
    //    {           
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid EmailTemplate");
    //            throw new HttpResponseException(response);
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            item.Active = true;
    //            item = repository.Add(item);
    //            response = Request.CreateResponse<EmailTemplate>(HttpStatusCode.Created, item);

    //            string uri = Url.Link("DefaultApi", new { id = item.EmailTemplateID });
    //            response.Headers.Location = new Uri(uri);                

    //        }
    //        else 
    //        {
    //            response = Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, ModelState.ToJson());                
    //        }

    //        return response;

    //    }

    //    public override HttpResponseMessage Put(EmailTemplate item)
    //    {
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid EmailTemplate");
    //            throw new HttpResponseException(response);
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            if (!repository.Update(item))
    //            {
    //                response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //                response.Content = new StringContent("Invalid EmailTemplate");
    //                throw new HttpResponseException(response);
    //            }
    //            else
    //            {
    //                response = Request.CreateResponse<EmailTemplate>(HttpStatusCode.OK, item);                    
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
    //        EmailTemplate item = repository.Get(id);
    //        HttpResponseMessage response;

    //        if (item == null)
    //        {
    //            response = new HttpResponseMessage(HttpStatusCode.NotFound);
    //            response.Content = new StringContent("Invalid EmailTemplate");
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



