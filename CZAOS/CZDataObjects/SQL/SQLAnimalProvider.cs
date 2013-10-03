using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using KT.Extensions;

namespace CZDataObjects
{
	public class SQLAnimalProvider :  BaseSQLAccess, IAnimalProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public SQLAnimalProvider()
		{
		}   			

		#endregion

        public List<Animal> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<Animal> items = new List<Animal>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_Animal_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    while (reader.Read())
                    {
                        Animal item = new Animal(reader);
                        items.Add(item);
                    }
                }

                return items;

            }
		}

        public Animal GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Animal item = null;

                base.AddParameter("@AnimalID", id);

                using (DbDataReader reader = base.ExecuteReader("czt_Animal_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
                {
                    if (reader.Read())
                    {
                        item = new Animal(reader);
                    }
                }

                return item;
            }
		}

        public Animal AddItem(Animal item)
		{
			this.AddParameters(item);  

			Animal newItem = null;

            using (DbDataReader reader = base.ExecuteReader("czt_Animal_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
            {
                if (reader.Read())
                {
                    newItem = new Animal(reader, true);
                }
            }

            //DataSet ds = base.ExecuteDataSet("czt_Animal_Insert", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
            //DataTable dt = ds.Tables[0];
            //int id = Convert.ToInt32( dt.Rows[0][0]);

            //newItem = GetItem(id);
			
			return newItem;		
		}

        public void UpdateItem(Animal item)
		{
            base.AddParameter("@AnimalID", item.AnimalID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Animal_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
            filterExpression = filterExpression.EnsureNotNull();
            return base.GetFilteredCount("czt_Animal_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@AnimalID", id);
            base.ExecuteNonQuery("czt_Animal_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(Animal item)
        {
            base.AddParameter("@AnimalRegionCode",item.AnimalRegionCode);
            base.AddParameter("@ZooID",item.ZooID);
            base.AddParameter("@CommonName",item.CommonName);
            base.AddParameter("@HouseName",item.HouseName);
            base.AddParameter("@ScientificName",item.ScientificName);
            base.AddParameter("@Gender",item.Gender);
            base.AddParameter("@DOB",item.DOB);
            base.AddParameter("@CZArrival",item.CZArrival);
            base.AddParameter("@Active",item.Active);
<<<<<<< HEAD
            base.AddParameter("@ExhibitID",item.ExhibitID);
=======
>>>>>>> Stopped Ignoring SQL folder
            
        }

	}
}   

	       
	    
	   