using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;
using System.Web;
using CZAOSCore.Enums;
using System.Web.Security;

namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class AdminNavigationList  : BaseList<AdminNavigation>
	{
		#region CONSTRUCTION/LOAD

		 public AdminNavigationList()
        {
        }

        public AdminNavigationList(List<AdminNavigation> items)
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
		public static AdminNavigationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("Folder");
			return new AdminNavigationList(AdminNavigationProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AdminNavigationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AdminNavigationList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AdminNavigationList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AdminNavigation GetItem(int id)
		{
			return AdminNavigationProvider.Instance().GetItem(id);
		}        

		public static int GetCount()
        {
            return AdminNavigationProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return AdminNavigationProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static AdminNavigation AddItem(AdminNavigation item)
        {         
			AdminNavigation nw = AdminNavigationProvider.Instance().AddItem(item);

            //Audit(nw, ChangeLog.LogChangeType.create, nw.ID);

            RemoveCacheList();

            return nw;                   
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(AdminNavigation item)
        {
            //AdminNavigation original = GetItem(item.ID);

            //AuditUpdate(original, item, item.ID);

            AdminNavigationProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(AdminNavigation item)
        {
            DeleteItem(item.ID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {    
			//Audit(typeof(AdminNavigation), ChangeLog.LogChangeType.delete, id);
            AdminNavigationProvider.Instance().DeleteItem(id);
            RemoveCacheList();           		
            
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void InitNav()
        {           

            string vpath = "/admin/";
            string ppath = HttpContext.Current.Server.MapPath(vpath);
            DirectoryInfo admin = new DirectoryInfo(ppath);

            if (admin.Exists)
            {
                AdminNavigationProvider.Instance().InitNav();
                RemoveCacheList();

                DirectoryInfo[] folders = admin.GetDirectories();

                foreach (DirectoryInfo folder in folders)
                {
                    AdminNavigation nav = new AdminNavigation();
                    nav.Folder = folder.Name;
                    nav.Roles = CoreUserTypeRoles.MasterAdmin.ToString();
                    nav.NavText = folder.Name;

                    AdminNavigationProvider.Instance().AddItem(nav);
                }
               
            }
            else
            {
                throw new Exception("could not find admin folder! fatal error!");
            }
            

        }

        //CACHED
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AdminNavigationList GetNavigation()
        {
            List<AdminNavigation> items = GetCacheList();

            if (items == null)
            {
                items = AdminNavigationList.GetItemCollection().ToList();
                AddToCache(items);
            }

            return new AdminNavigationList(items);
        }

        public static AdminNavigationList GetNavigationForUser()
        {
            List<AdminNavigation> items = GetNavigation();            
            string[] roles = Roles.GetRolesForUser();

            AdminNavigationList userNav = new AdminNavigationList();

            foreach (AdminNavigation nav in items)
            {
                foreach (string role in roles)
                {
                    if (nav.RoleList.Contains(role) || role.Equals(CZAOSCore.Enums.CoreUserTypeRoles.MasterAdmin.ToString()))
                    {
                        userNav.Add(nav);
                    }
                }
            }

            return userNav;
        }

        public static AdminNavigation GetItem(string folderName)
        {
            AdminNavigationList list = GetNavigation();
            return list.FirstOrDefault(s => s.Folder.Equals(folderName, StringComparison.InvariantCultureIgnoreCase));
        }
        
        #endregion

    }   

	
}



