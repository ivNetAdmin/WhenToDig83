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
        private RepositoryAsync<FrostDate> _frostDateRepository;

        public FrostManager()
        {
            _frostRepository = new RepositoryAsync<Frost>();
            _frostDateRepository = new RepositoryAsync<FrostDate>();           
        }

        internal async void Add(DateTime date)
        {

            //Random rnd = new Random();

            //var day = rnd.Next(1, 28);
            //var month = rnd.Next(1, 12);
            //var year = rnd.Next(2010, 2016);

            //Random rnd = new Random();

            var day = date.Day;
            var month = date.Month;
            var year = date.Year;

            var frost = await _frostRepository.Get(predicate: x => x.Month == month && x.Day == day);

            if (frost == null)
            {
                frost = new Frost
                {
                    Month = month,
                    Day = day,
                    Date = string.Format("{0}{1}", day.ToString("00"), DateHelper.MonthAbbreviation(month)),
                    Count = 1
                };
                await _frostRepository.Insert(frost);
            }
            else
            {
                frost.Count = frost.Count + 1;
                await _frostRepository.Update(frost);
            }

            await _frostDateRepository.Insert(new FrostDate { Date = new DateTime(year, month, day) });
        }

        internal async Task<List<FrostDate>> GetAllDates()
        {
            return await _frostDateRepository.Get();
        }

        internal async Task<List<Frost>> GetLastDates()
        {
            return await _frostRepository.Get(predicate: x => x.Month <= DateTime.Now.Month && x.Day <= DateTime.Now.Day, sortOrder: "desc", orderBy: x => x.Month, thenBy: x => x.Day, take: 6);
        }

        internal async Task<List<Frost>> GetNextDates()
        {
            return await _frostRepository.Get(predicate: x => x.Month >= DateTime.Now.Month && x.Day >= DateTime.Now.Day, sortOrder: "asc", orderBy: x => x.Month, thenBy: x => x.Day, take: 6);
        }
    }
}
