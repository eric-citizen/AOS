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
	[DataObject()] public class EmailTrackingList  : BaseList<EmailTracking>
	{
		#region CONSTRUCTION/LOAD

		 public EmailTrackingList()
        {
        }

         public EmailTrackingList(List<EmailTracking> items)
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
        public static EmailTrackingList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("SendDate");
            return new EmailTrackingList(EmailTrackingProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTrackingList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTrackingList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTrackingList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}			
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailTracking GetItem(int id)
		{
            return EmailTrackingProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return EmailTrackingProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return EmailTrackingProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static EmailTracking AddItem(EmailTracking item)
        {
            return EmailTrackingProvider.Instance().AddItem(item);           
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(int id)
        {
            EmailTrackingProvider.Instance().UpdateItem(id);
            
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(int id, bool sent, string failReason)
        {
            EmailTrackingProvider.Instance().UpdateItem(id, sent, failReason);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Crowd item)
        {          
            DeleteItem(item.CrowdID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            EmailTrackingProvider.Instance().DeleteItem(id);                        
        }

		#endregion

	}   

}



