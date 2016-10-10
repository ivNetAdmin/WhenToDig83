using System;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;
using WhenToDig83.Helpers;
using System.Collections.Generic;
using WhenToDig83.Core.Helpers;

namespace WhenToDig83.Managers
{
    public class FrostManager
    {
        private RepositoryAsync<Frost> _frostRepository;

        public FrostManager()
        {
            _frostRepository = new RepositoryAsync<Frost>();
        }

        internal async void Add()
        {
            //var frost = new Frost {
            //    Year = DateTime.Now.Year,
            //    Month = DateTime.Now.Month,
            //    Day = DateTime.Now.Day};
            //    await _frostRepository.Insert(frost);

            Random rnd = new Random();

            var day = rnd.Next(1, 28);
            var month = rnd.Next(1, 12);
            var frost = new Frost
            {
                Year = rnd.Next(2010, 2016),
                Month = month,
                Day = day,
                Date = string.Format("{0}{1}", day.ToString("00"), DateHelper.MonthAbbreviation(month))
            };
            await _frostRepository.Insert(frost);

             
        }

        internal async Task<List<Frost>> GetDates()
        {    
            return await _frostRepository.Get(predicate: x => x.Month >0, orderBy: x => x.Month);
        }
    }
}
