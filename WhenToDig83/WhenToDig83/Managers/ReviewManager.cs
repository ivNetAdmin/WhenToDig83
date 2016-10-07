
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
{
    public class ReviewManager
    {
        private RepositoryAsync<Note> _noteRepository;

        public ReviewManager()
        {
            _noteRepository = new RepositoryAsync<Note>();
        }

        internal async Task<List<Note>> Search(string searchTerm, int noteType = 0)
        {
            if(noteType==0)
                return await _noteRepository.Get(predicate: x => x.Notes.Contains(searchTerm), orderBy: x => x.Type);

            return await _noteRepository.Get(predicate: x => x.Type == noteType && x.Notes.Contains(searchTerm), orderBy: x => x.Type);           
        }
    }
}
