using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CZAOSCore
{
    /// <summary>
    /// This is an attempt to prevent having to repeatedly do the same analysis of the current request's URL
    /// from HTTP Modules and Handlers that execute on each request.
    /// </summary>
    [Serializable()]
    public class RequestProperties
    {
        private const string PROPERTIES_CONTEXT_KEY = "RequestProperties";

        public static RequestProperties Current
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    RequestProperties props = context.Items[PROPERTIES_CONTEXT_KEY] as RequestProperties;
                    if (props == null)
                    {
                        props = new RequestProperties();
                        context.Items[PROPERTIES_CONTEXT_KEY] = props;
                    }
                    return props;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Captures the raw URL of the original request
        /// </summary>
        public string RawUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// True if current request is for an .aspx page
        /// </summary>
        public bool IsPage
        {
            get;
            private set;
        }

        /// <summary>
        /// True if current request is for a managed resource (.aspx, .ashx, .asmx)
        /// </summary>
        public bool IsManagedHandler
        {
            get;
            private set;
        }

        /// <summary>
        /// True if the request is for a resource with no file extension
        /// </summary>
        public bool IsExtensionless
        {
            get;
            private set;
        }

        /// <summary>
        /// True if the request is for a real physical resource
        /// </summary>
        public bool PhysicalResourceExists
        {
            get;
            private set;
        }

        /// <summary>
        /// File extension of current request
        /// </summary>
        public string FileExtension
        {
            get;
            private set;
        }

        /// <summary>
        /// File name of current request without extension
        /// </summary>
        public string FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// The virtual path of the request excluding the app root and file name
        /// </summary>
        public string StrippedVirtualPath
        {
            get;
            private set;
        }

        public RequestProperties()
        {
            // Grab the current Request object
            HttpRequest request = HttpContext.Current.Request;
            HttpContext current = HttpContext.Current;

            string absolutePath = request.Url.AbsolutePath.ToLower();

            // Get file name, extension and virtual stripped path which get looked at a lot
            FileName = Path.GetFileNameWithoutExtension(absolutePath);
            FileExtension = Path.GetExtension(absolutePath);

            // Remove app root from beginning if there is one
            string applicationPath = request.ApplicationPath.ToLower();
            if (applicationPath != "/" && !string.IsNullOrEmpty(applicationPath))
                StrippedVirtualPath = absolutePath.Replace(request.ApplicationPath.ToLower(), "").Replace("//", "/").Trim('/');
            else
                StrippedVirtualPath = absolutePath;

            // Check if physical path exists
            try
            {
                PhysicalResourceExists = File.Exists(request.PhysicalPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("RequestProperties Exception: request.PhysicalPath is {0} - end", request.PhysicalPath), ex);
            }            

            // Set flags
            IsManagedHandler = (FileExtension.StartsWith(".aspx") || FileExtension.StartsWith(".ashx"));
            
            IsPage = (FileExtension.StartsWith(".aspx"));
            //isPage = (current.CurrentHandler is  System.Web.UI.Page); CurrentHandler always seems to be null

            IsExtensionless = string.IsNullOrEmpty(FileExtension);

            // Remove any file name from the end
            if (!IsExtensionless)
                StrippedVirtualPath = Path.GetDirectoryName(StrippedVirtualPath);

            // Ensure the virtual path has no junk at the beginning or end
            // And then replace any \ with / so it still looks like a URL
            StrippedVirtualPath = StrippedVirtualPath.Trim(new char[] { '/', '\\' }).Replace('\\', '/');

            RawUrl = request.RawUrl;

            CurrentHostname = request.Url.Host;

            // NEW for case 19884 - we need to support operating behind an F-5 Load balancer.  To determine if the connection is "secure" (delivered over ssl) we have
            //  to check both the http headers for the F-5 generated header AND the usual request.issecureconnection.
            string f5HeaderCheck = request.Headers["FRONT-END-HTTPS"];
            IsSecureConnection = (!string.IsNullOrEmpty(f5HeaderCheck) && f5HeaderCheck.Equals("on", StringComparison.InvariantCultureIgnoreCase)) || request.IsSecureConnection;
        }

        //public static string SanitizeFileName(string filename)
        //{
        //    string s = new string(filename.Where((System.Object x) => !Path.GetInvalidFileNameChars().Contains(x)).ToArray());
        //    s = s.Replace('\'', string.Empty);

        //    return s;

        //}

        public static string SanitizeFileName(string name)
        {
            return Path.GetInvalidFileNameChars().Aggregate(name, (current, c) => current.Replace(c, ' '));
        }

        public bool IsSecureConnection
        {
            get;
            private set;
        }

        public string CurrentHostname
        {
            get;
            private set;
        }
    }
}