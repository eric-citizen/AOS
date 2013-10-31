using System;
using System.Data;
using System.Data.Common;
using System.Web;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CZDataObjects
{

    //[Serializable()]
    public class AdminNavigation
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public AdminNavigation()
        {
        }

        public AdminNavigation(DbDataReader record) // : base(record)
        {
            mintID = record.Get<int>("ID");
            mstrFolder = HttpUtility.HtmlDecode(record.Get<string>("Folder"));
            mstrRoles = record.Get<string>("Roles");
            mstrNavText = HttpUtility.HtmlDecode(record.Get<string>("NavText"));
        }

        

        #endregion

        #region PROPERTIES

        private int mintID;
        private string mstrFolder = String.Empty;
        private string mstrRoles = String.Empty;
        private string mstrNavText = String.Empty;
        private List<string> roleList = new List<string>();

        public const char SEPERATOR = ',';

        [DataMember(Name = "ID")]
        public int ID
        {
            get
            {
                return mintID;
            }
            private set
            {
                mintID = value;
            }
        }

        [DataMember(Name = "Folder")]
        public string Folder
        {
            get
            {
                return mstrFolder;
            }
            set
            {
                mstrFolder = value.EnsureNotNull(250);
            }
        }

        [DataMember(Name = "NavText")]
        public string NavText
        {
            get
            {
                return mstrNavText;
            }
            set
            {
                mstrNavText = value.EnsureNotNull(250);
            }
        }

        [DataMember(Name = "Roles")]
        public string Roles
        {
            get
            {
                return mstrRoles;
            }
            set
            {
                mstrRoles = value.EnsureNotNull(2500);
            }
        }
       
        public List<string> RoleList
        {
            get
            {
                List<string> roleList = new List<string>(mstrRoles.Split(SEPERATOR));
                roleList.RemoveAll(x => x.IsNullOrEmpty());
                return roleList;
            }            
        }

        public void AddRole(string role)
        {
            List<string> roleList = this.RoleList;

            if (!roleList.Contains(role))
            {
                roleList.Add(role);
            }

            mstrRoles = roleList.ToString(",");
        }

        #endregion

    }

}



