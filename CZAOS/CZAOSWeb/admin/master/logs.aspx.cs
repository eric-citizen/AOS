using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;
using KT.Extensions;

namespace CZAOSWeb.admin.master
{
    public partial class logs : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.LoadData();

        }

        private void LoadData()
        {
            string physicalpath = Server.MapPath("~/logs/");
            DirectoryInfo dir = new DirectoryInfo(physicalpath);
            FileInfo[] files = dir.GetFiles();

            gvConfig.DataSource = files;
            gvConfig.DataBind();
             
        }

        protected void gvConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeleteLog"))
            {
                try
                {
                    string filename = e.CommandArgument.ToString();
                    string physicalpath = Server.MapPath("~/logs/" + filename);
                    File.Delete(physicalpath);

                    this.LoadData();

                }
                catch (Exception ex)
                {
                    this.DisplayError("Log Delete Error - " + ex.Message);
                }


            }
        }

        protected void confirmDeleteAll_Confirm(object sender, EventArgs e)
        {
            try
            {                
                string physicalpath = Server.MapPath("~/logs/");
                Array.ForEach(Directory.GetFiles(physicalpath), File.Delete);               

                this.LoadData();

            }
            catch (Exception ex)
            {
                this.DisplayError("Log Delete Error - " + ex.Message);
            }
        }

        protected void confirmDeleteEmpty_Confirm(object sender, EventArgs e)
        {
            try
            {            
                string physicalpath = Server.MapPath("~/logs/");
                DirectoryInfo dir = new DirectoryInfo(physicalpath);
                List<string> filesToDelete = new List<string>();

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Length == 0)
                    {
                        filesToDelete.Add(file.FullName);
                    }
                }

                foreach (string filename in filesToDelete)
                {
                    File.Delete(filename);
                }

                this.LoadData();

            }
            catch (Exception ex)
            {
                this.DisplayError("Log Delete Error - " + ex.Message);
            }

        }
        
        
    }
}