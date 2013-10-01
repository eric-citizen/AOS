using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using CZAOSCore;
using CZAOSCore.Logging;
using CZAOSCore.basepages;
using KT.Extensions;

namespace CZAOSWeb.admin.master
{   

    public partial class db : MainBase 
    {

        string db_backup_path = "/assets/db/";

        public class SQLSimpleAccess : CZDataObjects.BaseSQLAccess
        {
            string db_backup_path = "/assets/db/";
            public SQLSimpleAccess()
                : base()
            {
            }

            public DataSet Execute(string query)
            {
                return base.ExecuteDataSet(query);
            }

            public void BackUpDb()
            {
                string _DatabaseName = base.DatabaseName; // "CZAOS";
                string _BackupName = _DatabaseName + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".bak";
                StringBuilder query = new StringBuilder();                
                string dbPath = HttpContext.Current.Server.MapPath(db_backup_path);

                query.AppendFormat("BACKUP DATABASE {0} ", base.DatabaseName);
                query.AppendFormat("TO DISK = '{0}{1}' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '{1}';", dbPath, _BackupName);
                //string sqlQuery = "BACKUP DATABASE " + _DatabaseName + " TO DISK = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL10_50.MSSQLSERVER\\MSSQL\\Backup\\" + _BackupName + "' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + _BackupName + "';";

                base.ExecuteNonQuery(query.ToString(), CommandType.Text, CZDataObjects.ConnectionState.CloseOnExit);

            }

            public void RestoreDB(string backupDbName)
            {
                string _DatabaseName = base.DatabaseName;                 
                string _BackupName = backupDbName;
                string dbPath = HttpContext.Current.Server.MapPath(db_backup_path);
                StringBuilder query = new StringBuilder();

                query.AppendFormat("use master; RESTORE DATABASE {0} FROM DISK ='{1}{2}' WITH REPLACE", base.DatabaseName, dbPath, backupDbName);

                //string sqlQuery = "use master; RESTORE DATABASE " + _DatabaseName + " FROM DISK ='" + _BackupName + "' WITH REPLACE";                
                //RESTORE DATABASE AdventureWorks FROM DISK = 'C:\AdventureWorks.BAK'

                base.ExecuteNonQuery(query.ToString(), CommandType.Text, CZDataObjects.ConnectionState.CloseOnExit);                
            }

        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            string dbPath = Server.MapPath(db_backup_path);

            //Response.Write(dbPath + "<br/>");
            //string root = "/";
            //Response.Write(Server.MapPath(root));

            DirectoryInfo dbfolder = new DirectoryInfo(Server.MapPath(db_backup_path));
            FileInfo[] backups = dbfolder.GetFiles("*.bak");

            //foreach (FileInfo fi in backups)
            //{
            //    Response.Write(fi.Name + "<br/>");
            //}            

            if (backups.Length > 0)
            {
                ddlDB.Visible = true;
                btnRestore.Enabled = true;
                ddlDB.DataSource = backups;
                ddlDB.DataBind();
                ddlDB.AddEmptyListItem("--- Select Backup ---", "-1");

            }
            else
            {
                ddlDB.Visible = false;
                btnRestore.Visible = false;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsMasterAdmin)
            {
                Logger.Log(LogTarget.Security, MessageLevel.Info, "User " + User.Identity.Name + " attempted to load db manager page.");
                Response.Redirect("/admin/");
            }

            //Logger.Log(LogTarget.Security, MessageLevel.Info, User.Identity.Name + " attempted to load db manager page.");
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            SQLSimpleAccess access = new SQLSimpleAccess();
            msgError.Text = string.Empty;            

            try
            {
                gvResultSet.Visible = true;
                gvResultSet.DataSource = access.Execute(txtSQL.Text);
                gvResultSet.DataBind();

            }
            catch (Exception ex)
            {                
                //System361.Web.SiteTools.ErrorMessageHelper errHelper = new System361.Web.SiteTools.ErrorMessageHelper(ex);
                msgError.Text = ex.GetCompleteErrorMessageAsHTML(); // errHelper.ErrorHTML;
                gvResultSet.Visible = false;

            }
        }

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                SQLSimpleAccess access = new SQLSimpleAccess();
                msgError.Text = string.Empty;

                access.BackUpDb();

                msgError.Text = "Backup complete.";
                msgError.MessageType = KT.WebControls.MessageDiv.MessageTypes.success;
                
                //ReadBackupFiles();

            }
            catch (Exception sqlException)
            {
                msgError.Text = sqlException.GetCompleteErrorMessageAsHTML();
            }

        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            //CZAOS_10_7_2013.bak
            try
            {
                if (ddlDB.SelectedIndex > 0)
                {
                    SQLSimpleAccess access = new SQLSimpleAccess();
                    msgError.Text = string.Empty;

                    access.RestoreDB(ddlDB.SelectedValue);

                    msgError.Text = "Restore complete.";
                    msgError.MessageType = KT.WebControls.MessageDiv.MessageTypes.success;
                }
                

                //ReadBackupFiles();

            }
            catch (Exception sqlException)
            {
                msgError.Text = sqlException.GetCompleteErrorMessageAsHTML();
            }
        }
    }
}