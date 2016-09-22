
using System;
using System.Collections.Generic;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
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

        public List<WTDTask> GetTasksByMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month + 1, 1);
            return wtdTaskRepository.Get(predicate: x => x.Date >= startDate && x.Date < endDate, orderBy: x => x.Date);
        }

        public List<WTDTask> GetTasks()
        {
            return wtdTaskRepository.Get();
        }
    }
}
