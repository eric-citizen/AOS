using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using System.Web.Security;
using System.IO;

namespace CZAOSWeb.admin.services
{
    /// <summary>
    /// Summary description for services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class services : System.Web.Services.WebService
    {
        public services()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("you are not authenticated");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HelloWorld()
        {
            
            System.DateTime now = System.DateTime.Now;
            return "Hello World " + now.ToLongTimeString();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ExtendSession()
        {  
            System.DateTime now = System.DateTime.Now;

            if (Session != null)
            {
                Session.Add("SessionUpdate", now.ToLongTimeString());
                return new JavaScriptSerializer().Serialize("success " + now.ToLongTimeString());
            }

            return "Session null";

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool RegionExists(string regionId)
        {
            regionId = regionId.EnsureNotNull(3);

            if (regionId.IsNullOrEmpty())
            {
                return false;  
            }
            else
            {
                bool result = AnimalRegionList.RegionCodeExists(regionId);
                return result; // new JavaScriptSerializer().Serialize(result);
            }          

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool UserExists(string name)
        {
            name = name.EnsureNotNull();

            if (name.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                MembershipUser user = MembershipList.GetUser(name);
                return (user != null); 
            }

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool UserEmailExists(string email)
        {
            email = email.EnsureNotNull();

            if (email.IsNullOrEmpty() || !email.IsValidEmailAddress())
            {
                return false;
            }
            else
            {
                string user = MembershipList.GetUserNameByEmail(email);
                return (user.IsNotNullOrEmpty()); 
            }

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool TemplateKeyExists(string key, string id)
        {
            key = key.EnsureNotNull();
            id = id.EnsureNotNull();

            if (key.IsNullOrEmpty() || id.IsNullOrEmpty() || !id.IsNumeric())
            {
                return false;
            }
            else
            {
                int templateId = id.ToInt32();
                EmailTemplate et = EmailTemplateList.Get(key);

                if (templateId == 0)
                {
                    return (et != null);
                }                

                if(et == null)
                {
                    return false;
                }
                else
                {
                    return !et.ID.Equals(templateId);
                }
            }

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int GetTimeDifference(string start, string end)
        {

            //start = start.Replace("am","").Replace("pm","");
            //end = end.Replace("am","").Replace("pm","");
            try
            {
                DateTime s = Convert.ToDateTime(start);
                DateTime e = Convert.ToDateTime(end);

                return (int)e.Subtract(s).TotalMinutes; 
            }
            catch (Exception ex)
            {
                return 0;
            }                       

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public AnimalRegionList GetRegionList()
        {
            return AnimalRegionList.GetActiveItemCollection();
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public SchoolList GetSchoolList(string districtId)
        {
            if (districtId.IsNumeric())
            {
                return SchoolList.GetActiveSchoolsByDistrict(districtId.ToInt32());
            }

            return null;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ExhibitList GetExhibitList(string regionId)
        {
            if (regionId.IsNotNullOrEmpty())
            {
                return ExhibitList.GetActiveByRegion(regionId);
            }

            return null;
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool FolderExists(string folder)
        {
            folder = folder.EnsureNotNull();

            if (folder.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                folder = folder.EnsureStartsWith("/admin/");
                folder = Server.MapPath(folder);
                bool result = Directory.Exists(folder);
                return result;  
            }

        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLogFileContents(string filename)
        {
            filename = filename.EnsureNotNull();

            if (filename.IsNullOrEmpty())
            {
                return "filename is empty!";
            }
            else
            {
                filename = "~/logs/" + filename;
                filename = Server.MapPath(filename);

                FileInfo fi = new FileInfo(filename);

                if (fi.Exists)
                {
                    string s = "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    using (StreamReader sr = fi.OpenText())
                    {
                        
                        while ((s = sr.ReadLine()) != null)
                        {
                            sb.AppendFormat("{0}<br/>", s);
                        }

                        if (sb.Length == 0)
                        {
                            return "file is empty.";
                        }
                        else
                        {
                            return sb.ToString();
                        }
                        
                    }                   

                }
                else
                {
                    return filename + " not found!";
                }                 
                
            }

        }
    }
}
