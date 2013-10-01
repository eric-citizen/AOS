using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class NoteRepository : IObservationRepository<Note>
    {
        public IEnumerable<Note> GetAll()
        {
            List<Note> records = NoteList.GetActive();
            return records;
        }

        public IEnumerable<Note> GetAll(int observationId)
        {
            List<Note> records = NoteList.GetActive(observationId);
            return records;
        }

        public Note Get(int id)
        {
            return NoteList.Get(id);
        }

        public Note Add(Note item)
        {
            return NoteList.AddItem(item);
        }

        public void Remove(int id)
        {
            NoteList.DeleteItem(id);
        }

        public bool Update(Note item)
        {
            NoteList.UpdateItem(item);
            return true;
        }
    }
}
