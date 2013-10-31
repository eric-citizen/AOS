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
    [DataObject()]
    public class SchoolList : BaseList<School>
	{
		#region CONSTRUCTION/LOAD

		 public SchoolList()
        {
        }

        public SchoolList(List<School> items)
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
		public static SchoolList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("School");
			return new SchoolList(SchoolProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolList GetItemCollection(bool active)
		{
			string filter = string.Empty;
			
			if(active)
			{
				filter = "Active = 1";
			}
			else
			{
				filter = "Active = 0";
			}
				
			return GetItemCollection(0, 0, "School ASC", filter);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static School GetItem(int id)
		{
			return SchoolProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return SchoolProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return SchoolProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static School AddItem(School item)
        {
            School school = SchoolProvider.Instance().AddItem(item);
            RemoveCacheList();
            Audit(school, ChangeLog.LogChangeType.create, school.SchoolID);
            return school;
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(School item)
        {
            School original = GetSchool(item.SchoolID);
            AuditUpdate(original, item, item.SchoolID);      

            SchoolProvider.Instance().UpdateItem(item);
            RemoveCacheList();
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(School item)
        {          
            DeleteItem(item.SchoolID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            Audit(typeof(School), ChangeLog.LogChangeType.delete, id);
            SchoolProvider.Instance().DeleteItem(id);
            RemoveCacheList();
        }

        public static bool ItemExists(string school, int districtId, int itemId)
        {
            SchoolList list = GetItemCollection(0, 1, "", "School = '{0}'".FormatWith(school));
            return list.Any(x => (x.DistrictID == districtId && x.SchoolID != itemId));
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SchoolList GetActiveSchools()
        {
            List<CZDataObjects.School> schools = GetCacheList();

            if (schools == null)
            {
                schools = SchoolList.GetItemCollection(true).OrderBy(x => x.SchoolName).ToList();
                AddToCache(schools);
            }

            return new SchoolList(schools);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SchoolList GetActiveSchoolsByDistrict(int districtId)
        {
            SchoolList schools = GetActiveSchools();

            IEnumerable<School> schoolsByDistrict = schools.Where(
                    s => s.DistrictID == districtId);

            return new SchoolList(schoolsByDistrict.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static School GetSchool(int id)
        {
            SchoolList schools = GetActiveSchools();
            School school = schools.FirstOrDefault(s => s.SchoolID == id);

            return school;
        }  

		#endregion        

	}   

}



