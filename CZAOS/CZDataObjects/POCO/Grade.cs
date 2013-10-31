using System;
using System.Data;
using System.Data.Common;
using System.Web;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CZDataObjects
{

    public class Grade
    {
        #region CONSTRUCTORS/DESTRUCTORS

        public Grade()
        {
        }

        public Grade(DbDataReader record) // : base(record)
        {
            mintGradeID = record.Get<int>("GradeID");
            mstrGrade = HttpUtility.HtmlDecode(record.Get<string>("Grade"));
            mblnActive = record.Get<bool>("Active");

        }

        public Grade(DataRow row)
        {

        }

        #endregion

        #region PROPERTIES

        private int mintGradeID;
        private string mstrGrade = String.Empty;
        private bool mblnActive;

        public int GradeID
        {
            get
            {
                return mintGradeID;
            }
        }

        public string GradeName
        {
            get
            {
                return mstrGrade;
            }
            set
            {
                mstrGrade = value.EnsureNotNull(100);
            }
        }

        public bool Active
        {
            get
            {
                return mblnActive;
            }
            set
            {
                mblnActive = value;
            }
        }

        #endregion

    }

}



