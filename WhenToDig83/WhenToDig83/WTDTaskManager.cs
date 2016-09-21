
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;

namespace WhenToDig83
{
    public class WTDTaskManager
    {
        Repository<WTDTask> wtdTaskRepository;

        public WTDTaskManager()
        {
            wtdTaskRepository = new Repository<WTDTask>();
        }       
    }
}
