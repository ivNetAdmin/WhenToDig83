
using System.Collections.Generic;
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
        
        public void AddTask(string name, System.DateTime date, string type)
        {
            wtdTaskRepository.Insert(new WTDTask { Name = name, Date = date, Type = type });
        }

        public List<WTDTask> GetTasks()
        {
            return wtdTaskRepository.Get();
        }
    }
}
