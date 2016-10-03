using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
