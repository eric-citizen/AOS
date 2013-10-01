using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class EmailTrackingProvider  
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public EmailTrackingProvider()
			{
			}   

		#endregion

        private static IEmailTrackingProvider _instance = null;

        public static IEmailTrackingProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
                _instance = new SQLEmailTrackingProvider();
			}

			return _instance;
		}

	}

    public interface IEmailTrackingProvider
	{
        List<EmailTracking> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        EmailTracking GetItem(int id);
        EmailTracking AddItem(EmailTracking item);
        void UpdateItem(int id);
        void UpdateItem(int id, bool sent, string failReason);
        //czt_EmailTracking_UpdateSend
		int GetCount(string filterExpression);
        
		void DeleteItem(int id);
	}

}


