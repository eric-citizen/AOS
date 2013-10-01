using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class NoteProvider 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public NoteProvider()
			{
			}   

		#endregion

		private static INoteProvider _instance = null;

		public static INoteProvider Instance()
		{
            if (_instance == null)
            {
                _instance = new SQLNoteProvider();
            }

            return _instance;
		}

	}    

	public interface INoteProvider
	{
		List<Note> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Note GetItem(int id);

		Note AddItem(Note item);
		void UpdateItem(Note item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


