using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;

namespace CZBizObjects
{
	[Serializable()]
    [DataObject()]
    public class AnimalList : BaseList<Animal>
	{
		#region CONSTRUCTION/LOAD

		public AnimalList()
        {
        }

        public AnimalList(List<Animal> items)
        {
            base.AddRange(items);
        }

        public void Load()
        {
            this.AddRange(GetItemCollection());
        }
		#endregion

		#region ADMIN METHODS

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("CommonName"); 
			return new AnimalList(AnimalProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Animal GetItem(int id)
		{
			return AnimalProvider.Instance().GetItem(id);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Animal GetItemByZooID(string id)
        {
            return AnimalProvider.Instance().GetItemByZooID(id);
        }

		public static int GetCount()
        {
            return AnimalProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return AnimalProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Animal AddItem(Animal item)
        {
            Animal nw = AnimalProvider.Instance().AddItem(item);

            Audit(nw, ChangeLog.LogChangeType.create, nw.AnimalID);

            RemoveCacheList();

            return nw;            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Animal item)
        {
            Animal original = GetAnimal(item.AnimalID);

            AuditUpdate(original, item, item.AnimalID);

            AnimalProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Animal item)
        {          
            DeleteItem(item.AnimalID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            Audit(typeof(Animal), ChangeLog.LogChangeType.delete, id);
            AnimalProvider.Instance().DeleteItem(id);
            RemoveCacheList();            
        }

		#endregion

        #region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (active)
            {
                filter = "Active = 1";
            }
            else
            {
                filter = "Active = 0";
            }

            return GetItemCollection(0, 0, "CommonName ASC", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalList GetActiveAnimals()
        {
            List<CZDataObjects.Animal> animals = GetCacheList();

            if (animals == null)
            {
                animals = AnimalList.GetItemCollection(true).ToList();
                AddToCache(animals);
            }

            return new AnimalList(animals);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalList GetActiveAnimals(string animalRegionCode)
        {
            List<CZDataObjects.Animal> animals = GetActiveAnimals();
            IEnumerable<Animal> items = animals.Where(a => a.AnimalRegionCode.Equals(animalRegionCode));           

            return new AnimalList(items.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Animal GetAnimal(int id)
        {
            AnimalList animals = GetActiveAnimals();
            Animal animal = animals.FirstOrDefault(s => s.AnimalID == id);

            return animal;
        }

        #endregion     
	}   

}



