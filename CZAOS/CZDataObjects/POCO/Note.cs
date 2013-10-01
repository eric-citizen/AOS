using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KT.Extensions;
using System.Data;
using System.Data.Common;


namespace CZDataObjects
{
	//[Serializable()]
	public class Note 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public Note()
			{
			}
    
			public Note(DbDataReader record) 
			{
                this.NoteID = record.Get<int>("NoteID");
                mintObservationID = record.Get<int>("ObservationID");
                mstrUsername = record.Get<string>("Username");
                mdteNoteTime = record.Get<DateTime>("NoteTime");
                mstrNote = record.Get<string>("Note");
                mblnDeleted = record.Get<bool>("Deleted");
			}	
						
			
		#endregion

		#region PROPERTIES			
        
        private int mintObservationID;
        private string mstrUsername = String.Empty;
        private DateTime mdteNoteTime;
        private string mstrNote = String.Empty;
        private bool mblnDeleted;
			
        public int NoteID
        {
            get;private set;            
        }

        public int ObservationID
        {
            get
            {
                return mintObservationID;
            }
            set
            {
                mintObservationID = value;
            }
        }

        public string Username
        {
            get
            {
                return mstrUsername;
            }
            set
            {
                mstrUsername = value.EnsureNotNull(20);
            }
        }

        public DateTime NoteTime
        {
            get
            {
                return mdteNoteTime;
            }
            set
            {
                mdteNoteTime = value;
            }
        }

        public string NoteText
        {
            get
            {
                return mstrNote;
            }
            set
            {
                mstrNote = value.EnsureNotNull(250);
            }
        }

        public bool Deleted
        {
            get
            {
                return mblnDeleted;
            }
            set
            {
                mblnDeleted = value;
            }
        }

		#endregion

	}

}



