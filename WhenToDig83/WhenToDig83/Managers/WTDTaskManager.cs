
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
        private RepositoryAsync<WTDTask> _wtdTaskRepository;

        public WTDTaskManager()
        {
            _wtdTaskRepository = new RepositoryAsync<WTDTask>();
        }

        public async Task<int> AddTask(string name, DateTime date, string type)
        {
           return await _wtdTaskRepository.Insert(new WTDTask { Name = name, Date = date, Type = type });
        }

        public async Task<List<WTDTask>> GetTasksByMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month + 1, 1);
            return await _wtdTaskRepository.Get(predicate: x => x.Date >= startDate && x.Date < endDate, orderBy: x => x.Date);
        }

        public List<WTDTask> GetTasks()
        { 
            return new List<WTDTask>();
        }
    }
}
