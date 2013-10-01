using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KT.Extensions;
using CZBizObjects;
using CZAOSCore.Logging;

namespace CZAOSMail.Handler
{
    public class OpenMailHandler : System.Web.IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if(context.Request.QueryString[0] != null)
                {
                    string id = context.Request.QueryString[0];

                    if (id.IsNumeric())
                    {
                        int mailId = Convert.ToInt32(id);
                        EmailTrackingList.UpdateItem(mailId);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogError(ErrorLevel.Error, ex, false);
            }
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
