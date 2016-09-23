
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
{
    public class WTDTaskManager
    {
        RepositoryAsync<WTDTask> wtdTaskRepository;

        public WTDTaskManager()
        {
            wtdTaskRepository = new RepositoryAsync<WTDTask>();
        }

        public async void AddTask(string name, System.DateTime date, string type)
        {
            await wtdTaskRepository.Insert(new WTDTask { Name = name, Date = date, Type = type });
        }

        public async Task<IEnumerable<WTDTask>> GetTasksByMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month + 1, 1);
            return await wtdTaskRepository.Get(predicate: x => x.Date >= startDate && x.Date < endDate, orderBy: x => x.Date);
        }

        public List<WTDTask> GetTasks()
        { 
            return new List<WTDTask>();
        }
    }
}
