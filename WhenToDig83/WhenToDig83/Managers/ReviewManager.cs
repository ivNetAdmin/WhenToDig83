

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
    }
}
