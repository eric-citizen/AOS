using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class EmailTemplateProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public EmailTemplateProvider()
			{
			}   

		#endregion

		private static IEmailTemplateProvider _instance = null;

		public static IEmailTemplateProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLEmailTemplateProvider();
			}

			return _instance;
		}

	}    

	public interface IEmailTemplateProvider
	{
		List<EmailTemplate> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		EmailTemplate GetItem(int id);

		EmailTemplate AddItem(EmailTemplate item);
		void UpdateItem(EmailTemplate item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


