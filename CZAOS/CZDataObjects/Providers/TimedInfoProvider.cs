using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class TimedInfoProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public TimedInfoProvider()
			{
			}   

		#endregion

        private static ITimedInfoProvider _instance = null;

        public static ITimedInfoProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
                _instance = new SQLTimedInfoProvider();
			}

			return _instance;
		}

	}

    public interface ITimedInfoProvider
	{
        List<TimedInfo> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        TimedInfo GetItem(int id);

        TimedInfo AddItem(TimedInfo item);
        void UpdateItem(TimedInfo item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


