
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
{
    public class NoteManager
    {
        private RepositoryAsync<Note> _noteRepository;

        public NoteManager()
        {
            _noteRepository = new RepositoryAsync<Note>();
        }

        //public async Task<int> AddNote(int type, int typeId, string notes, string meta)
        //{
        //    var note = await _noteRepository.Get(predicate: x => x.TypeId == typeId && x.Type == type);

        //    if (note == null)
        //    {
        //        return await _noteRepository.Insert(new Note { Type = type, TypeId = typeId, Notes = notes });
        //    }
        //    else
        //    {                
        //        note.Type = type;
        //        note.TypeId = typeId;
        //        note.Notes = notes;
        //        return await _noteRepository.Update(note);
        //    }
        //}

        public async Task<Note> GetNote(int type, int typeId)
        {
            return await _noteRepository.Get(predicate: x => x.TypeId == typeId && x.Type == type);
        }
    }
}
