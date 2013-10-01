using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;
using System.IO;
using CZAOSCore.Logging;

namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class ObservationReportList  : BaseList<ObservationReport>
	{
		#region CONSTRUCTION/LOAD

		 public ObservationReportList()
        {
        }

        public ObservationReportList(List<ObservationReport> items)
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
		public static ObservationReportList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObservationID, ReportName");
			return new ObservationReportList(ObservationReportProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationReportList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationReportList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationReportList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationReport GetItem(int id)
		{
			return ObservationReportProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ObservationReportProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ObservationReportProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static ObservationReport AddItem(ObservationReport item)
        {
            ObservationReport nw = ObservationReportProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ReportID);
            RemoveCacheList();

            return nw;                
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(ObservationReport item)
        {
            ObservationReport original = Get(item.ReportID);

            AuditUpdate(original, item, item.ReportID);

            ObservationReportProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(ObservationReport item)
        {
            DeleteItem(item.ReportID, item.ReportLink);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id, string fileVirtualPath)
        {
            ObservationReportProvider.Instance().DeleteItem(id);
            Audit(typeof(ObservationReport), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();

            try 
            {
                string physical = System.Web.HttpContext.Current.Server.MapPath(fileVirtualPath);
                File.Delete(physical);

            }
            catch (Exception ex)
            {
                Logger.LogError(ErrorLevel.Error, "Could not delete report file " + fileVirtualPath, ex, false);
            }
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationReportList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (!active)
            {
                filter = "Deleted = 1";
            }
            else
            {
                filter = "Deleted = 0";
            }

            return GetItemCollection(0, 0, "ObservationID, ReportName", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationReportList GetActive()
        {
            List<ObservationReport> items = GetCacheList();

            if (items == null)
            {
                items = ObservationReportList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ObservationReportList(items);
        }
        
        public static ObservationReportList GetActive(int observationId)
        {
            List<ObservationReport> items = GetActive();

            IEnumerable<ObservationReport> reps = items.Where(r => r.ObservationID == observationId);

            return new ObservationReportList(reps.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationReport Get(int id)
        {
            ObservationReportList items = GetActive();
            ObservationReport item = items.FirstOrDefault(s => s.ReportID == id);

            return item;
        }

		#endregion

	}   

}



