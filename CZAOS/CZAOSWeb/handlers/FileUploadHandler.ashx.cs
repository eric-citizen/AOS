using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using KT.Extensions;
using System.Web.SessionState;
using CZBizObjects;
using CZDataObjects;

namespace CZAOSWeb.handlers
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler, IReadOnlySessionState
    {

        private static readonly char[] InvalidfilenameCharacters = Path.GetInvalidFileNameChars();       

        private static string SanitizeFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            return System.Text.RegularExpressions.Regex.Replace(name, invalidReStr, "_");
        }

        private string RemovePeriodsFromFilename(string originalFilename)
        {

            string filename = Path.GetFileNameWithoutExtension(originalFilename);
            string sanitized = filename.Replace(".", string.Empty);
            string ext = Path.GetExtension(originalFilename);

            return Path.Combine(sanitized + ext);

        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            int observationId = context.Session.Get<int>(SessionKeys.UploadFileFolder); // request.Headers["observationId"];

            if (observationId == 0)
            {
                //bail
                this.SendResult(context, "observation Id not found");
                return;

            }

            if (request.Files.Count > 0)
            {               

                int chunk = request["chunk"] != null ? int.Parse(request["chunk"]) : 0;
                HttpPostedFile fileUpload = request.Files[0];
                string fileName = fileUpload.FileName;

                fileName = SanitizeFileName(fileName);
                fileName = RemovePeriodsFromFilename(fileName);

                SysCode config = SysCodeList.Get("UploadFileFolder");

                if (config == null)
                {
                    this.SendResult(context, "UploadFileFolder config not found!");
                    return;
                }

                string strUploadFolder = config.Value;
                strUploadFolder = strUploadFolder.EnsureEndsWith("/");
                strUploadFolder += observationId + "/";
 
                string uploadPath = context.Server.MapPath(strUploadFolder);
                DirectoryInfo dir = new DirectoryInfo(uploadPath);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                using (FileStream fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
                {                    
                    byte[] buffer = new byte[Convert.ToInt32(fileUpload.InputStream.Length - 1) + 1];

                    fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                    fs.Write(buffer, 0, buffer.Length);

                }

                ObservationReport report = new ObservationReport();
                report.ObservationID = observationId;
                report.ReportLink = "{0}{1}".FormatWith(strUploadFolder, fileName); // Path.Combine(uploadPath, fileName);
                report.ReportName = fileName;

                ObservationReportList.AddItem(report);
                
                this.SendResult(context, "success");
                //context.Response.ContentType = "text/plain";
                //context.Response.Write("success");

            }
            else
            {
                this.SendResult(context, "No files found!");
            }

            //=======================================================
            //Service provided by Telerik (www.telerik.com)
            //Conversion powered by NRefactory.
            //Twitter: @telerik, @toddanglin
            //Facebook: facebook.com/telerik
            //=======================================================

        }

        private void SendResult(HttpContext context, string message)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(message);
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}