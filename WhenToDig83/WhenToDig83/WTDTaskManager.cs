
using System;
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

        internal object GetTasks(int month)
        {
            return wtdTaskRepository.Get(predicate: x => x.Name == "Test", orderBy: x => x.Name);
            return wtdTaskRepository.Get(predicate: x => x.Date.Month == month, orderBy: x => x.Date);
        }

        public List<WTDTask> GetTasks()
        {
            return wtdTaskRepository.Get();
        }
    }
}
