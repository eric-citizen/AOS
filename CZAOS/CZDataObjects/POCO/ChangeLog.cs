using System;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web;
using System.Reflection;
using CZDataObjects.Extensions;

namespace CZDataObjects
{

	//[Serializable()]
    public class ChangeItem
    {
        public string FieldName { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
    }

	public class ChangeLog   
	{

        public enum LogChangeType
        {
            undefined,
            create,
            update,
            delete,
            sort
        }

		#region CONSTRUCTORS/DESTRUCTORS

			public ChangeLog()
			{
                _dataDictionary = new Dictionary<string, string>();
			}
    
			public ChangeLog(DbDataReader record) // : base(record)
			{
				mintID = record.Get<int>("ID");
                mdteChangeDate = record.Get<DateTime>("ChangeDate");
                mUserID = record.Get<Guid>("UserID"); // new Guid(record.Get<Guid>("UserID"));
                mstrUserDisplayName = record.Get<string>("UserDisplayName");

                string _changeType = record.Get<string>("ChangeType");
                foreach (LogChangeType ct in Enum.GetValues(typeof(LogChangeType)))
                {
                    if (ct.ToString().Equals(_changeType))
                    {
                        mChangeType = ct;
                    }
                }

                mstrIdentifier = record.Get<string>("Identifier");
                mstrChanges = record.Get<string>("Changes");

                _dataDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(mstrChanges);
          
			}

			public ChangeLog(DataRow row)  
			{

			}

		#endregion

		#region PROPERTIES
			
        private int mintID;
        private DateTime mdteChangeDate;
        private Guid mUserID;
        private string mstrUserDisplayName = String.Empty;
        private LogChangeType mChangeType;
        private string mstrIdentifier = String.Empty;
        private string mstrChanges = String.Empty;

        private Dictionary<string, string> _dataDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> _updatedValues = new Dictionary<string, string>();
        private Dictionary<string, string> _changedValues = new Dictionary<string, string>();
        
			
        [DataMember(Name = "ID")]
        public int ID
        {
            get
            {
                return mintID;
            }           
        }

        [DataMember(Name = "ChangeDate")]
        public DateTime ChangeDate
        {
            get
            {
                return mdteChangeDate;
            }           
        }

        [DataMember(Name = "UserID")]
        public Guid UserID
        {
            get
            {
                return mUserID;
            }
            set
            {
                mUserID = value;
            }
        }

        [DataMember(Name = "UserDisplayName")]
        public string UserDisplayName
        {
            get
            {
                return mstrUserDisplayName;
            }
            set
            {
                mstrUserDisplayName = value.EnsureNotNull(100);
            }
        }

        [DataMember(Name = "ChangeType")]
        public LogChangeType ChangeType
        {
            get
            {
                return mChangeType;
            }
            set
            {
                mChangeType = value;
            }
        }

        [DataMember(Name = "ChangeTypeText")]
        public string ChangeTypeText
        {
            get
            {
                return mChangeType.ToString();
            }            
        }

        [DataMember(Name = "Identifier")]
        public string Identifier
        {
            get
            {
                return mstrIdentifier;
            }
            set
            {
                mstrIdentifier = value.EnsureNotNull(100);
            }
        }

        
        [DataMember(Name = "Changes")]
        public string Changes
        {
            get
            {
                mstrChanges = new JavaScriptSerializer().Serialize(_changedValues);
                return mstrChanges;
            }
        }
        
        public List<ChangeItem> ChangeItemList
        {
            //  {"Description":"original value: xxx - new value: xxx2","Active":"original value: True - new value: False"}
            get
            {
                List<ChangeItem> changes = new List<ChangeItem>();
                foreach (string field in _dataDictionary.Keys)
                {
                    ChangeItem item = new ChangeItem();
                    item.FieldName = field;
                    string values = _dataDictionary[field];
                    string og = "original value: ";
                    string nv = " - new value: ";

                    int start = values.IndexOf(og);
                    int end = values.IndexOf(" - ", start);

                    item.Old = values.Substring(start, end).Replace(og, string.Empty);

                    start = values.IndexOf(nv);

                    item.New = values.Substring(start).Replace(nv, string.Empty);

                    changes.Add(item);

                }
                 
                return changes;
            }
        }

        public Dictionary<string, string> DataDictionary
        {
            get
            {
                if ((_dataDictionary == null))
                {
                    _dataDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(mstrChanges);
                }

                return _dataDictionary;
            }
        }

        public Int32 DataItemCount
        {
            get
            {
                if ((_dataDictionary == null))
                {
                    _dataDictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(mstrChanges);
                }

                return _dataDictionary.Count;
            }
        }

        public Dictionary<string, string> UpdatedValues
        {
            get { return _updatedValues; }
        }

        public Dictionary<string, string> ChangedValues
        {
            get { return _changedValues; }
        }

		#endregion

        public void CreateIdentifier(object o, int id)
        {
            string format = "Object Type: {0}, ID: {1}";
            Type theType = o.GetType();
            mstrIdentifier = format.FormatWith(theType.Name, id);           
        }

        public void CreateIdentifier(Type type, int id)
        {
            string format = "Object Type: {0}, ID: {1}";            
            mstrIdentifier = format.FormatWith(type.Name, id);
        }

        public void CreateIdentifier(object o, string id)
        {
            string format = "Object Type: {0}, ID: {1}";
            Type theType = o.GetType();
            mstrIdentifier = format.FormatWith(theType.Name, id);
        }

        public void CreateIdentifier(Type type, string id)
        {
            string format = "Object Type: {0}, ID: {1}";
            mstrIdentifier = format.FormatWith(type.Name, id);
        }

        public void SaveToSession(string key)
        {
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

            if ((session != null))
            {
                session.Set<Dictionary<string, string>>(key, _dataDictionary);
            }

        }


        public void LoadFromSession(string key)
        {
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

            if ((session != null))
            {
                _dataDictionary = session.Get<Dictionary<string, string>>(key);
            }

        }


        public void ParseObject(object obj)
        {
            _dataDictionary = new Dictionary<string, string>();


            foreach (string field in this.GetObjectFields(obj))
            {
                if (!_dataDictionary.ContainsKey(field))
                {
                    _dataDictionary.Add(field, Convert.ToString(this.GetObjectPropertyValue(obj, field)));
                }

            }

        }
        public void ParseObject(Dictionary<string, string> additionalValues)
        {
            _dataDictionary = new Dictionary<string, string>();


            foreach (string field in additionalValues.Keys)
            {
                if (!_dataDictionary.ContainsKey(field))
                {
                    _dataDictionary.Add(field, additionalValues[field]);
                }

            }

        }

        public void AddChangedValue(string field)
        {
            //String.Format("original value: {0} - new value: {1}", _dataDictionary(field), strUpdatedValue)
            _changedValues.Add(field, string.Format("original value: {0} - new value: {1}", _dataDictionary[field], _updatedValues[field]));

        }        

        public bool CompareObject(Dictionary<string, string> additionalValues)
        {


            foreach (string field in additionalValues.Keys)
            {
                if (!_updatedValues.ContainsKey(field))
                {
                    _updatedValues.Add(field, additionalValues[field]);
                }

            }


            foreach (string field in _dataDictionary.Keys)
            {

                if (_updatedValues.ContainsKey(field))
                {
                    if (!_dataDictionary[field].Equals(_updatedValues[field], StringComparison.InvariantCultureIgnoreCase))
                    {
                        _changedValues.Add(field, string.Format("original value: {0} - new value: {1}", _dataDictionary[field], _updatedValues[field]));
                    }

                }

            }

            return _changedValues.Count > 0;

        }

        public bool CompareObject(object obj)
        {


            foreach (string field in this.GetObjectFields(obj))
            {

                if (_dataDictionary.ContainsKey(field))
                {
                    string strUpdatedValue = Convert.ToString(this.GetObjectPropertyValue(obj, field));
                    if (!_dataDictionary[field].Equals(strUpdatedValue, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _changedValues.Add(field, string.Format("original value: {0} - new value: {1}", _dataDictionary[field], strUpdatedValue));
                    }

                }

            }

            return _changedValues.Count > 0;

        }


        public bool CompareObject(object original, object compare)
        {

            this.ParseObject(original);

            foreach (string field in this.GetObjectFields(compare))
            {
                if (_dataDictionary.ContainsKey(field))
                {
                    string strUpdatedValue = Convert.ToString(this.GetObjectPropertyValue(compare, field));
                    if (!_dataDictionary[field].Equals(strUpdatedValue, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _changedValues.Add(field, string.Format("original value: {0} - new value: {1}", _dataDictionary[field], strUpdatedValue));
                    }

                }

            }

            return _changedValues.Count > 0;

        }
        private List<string> GetObjectFields(object obj)
        {

            Type theType = obj.GetType();
            PropertyInfo[] myProperties = theType.GetProperties((BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly));
            List<string> fields = new List<string>();


            foreach (PropertyInfo pi in myProperties)
            {
                if (pi.CanWrite)
                {
                    fields.Add(pi.Name);
                }

            }

            return fields;

        }

        private object GetObjectPropertyValue(object obj, string propertyName)
        {

            Type theType = obj.GetType();
            PropertyInfo[] myProperties = theType.GetProperties((BindingFlags.Public | BindingFlags.Instance));


            foreach (PropertyInfo pi in myProperties)
            {
                if (pi.CanWrite)
                {
                    if (pi.Name.ToLower().Equals(propertyName.ToLower()))
                    {
                        return pi.GetValue(obj, null);
                    }
                }

            }

            return string.Format("Property Not Found [{0}]", propertyName);

        }

	}

}



