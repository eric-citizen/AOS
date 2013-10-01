using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class SysCodeProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SysCodeProvider()
			{
			}   

		#endregion

		private static ISysCodeProvider _instance = null;

		public static ISysCodeProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLSysCodeProvider();
			}

			return _instance;
		}

	}    

	public interface ISysCodeProvider
	{
		List<SysCode> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		//SysCode GetItem(int id);

		SysCode AddItem(SysCode item);
		void UpdateItem(SysCode item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


