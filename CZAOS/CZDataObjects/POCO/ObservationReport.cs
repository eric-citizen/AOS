using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

    public class ObservationReport
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public ObservationReport()
        {
        }

        public ObservationReport(DbDataReader record) // : base(record)
        {
            this.ReportID = record.Get<int>("ReportID");
            this.ObservationID = record.Get<int>("ObservationID");
            mstrReportName = record.Get<string>("ReportName");
            mstrReportLink = record.Get<string>("ReportLink");
            mblnDeleted = record.Get<bool>("Deleted");
        }

        
        #endregion

        #region PROPERTIES
        
        private string mstrReportName = String.Empty;
        private string mstrReportLink = String.Empty;
        private bool mblnDeleted;

        public int ReportID
        {
            get;private set;            
        }

        public int ObservationID
        {
            get;set;            
        }

        public string ReportName
        {
            get
            {
                return mstrReportName;
            }
            set
            {
                mstrReportName = value.EnsureNotNull(100);
            }
        }

        public string ReportLink
        {
            get
            {
                return mstrReportLink;
            }
            set
            {
                mstrReportLink = value.EnsureNotNull(250);
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



