using System;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Data;
using WhenToDig83.Helpers;
using System.Collections.Generic;

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
            for(int i=0;i<50;i++)
            {  
               var frost = new Frost
                {
                    Year = rnd.Next(2010,2016),
                    Month = rnd.Next(1, 12),
                    Day = rnd.Next(1, 28)
                };
                await _frostRepository.Insert(frost);
            }
             
        }

        internal async Task<List<Frost>> GetDates()
        {
            var d = await _frostRepository.Get();
            return d;
        }
    }
}
