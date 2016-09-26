
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
{
    public class PlantManager
    {
        private RepositoryAsync<Plant> _plantRepository;

        public PlantManager()
        {
            _plantRepository = new RepositoryAsync<Plant>();
        }
        
        public async void AddPlant(string name)
        {
            await _plantRepository.Insert(new Plant { Name = name });
        }
        
        public async Task<List<Plant>> GetPlants()
        { 
            return await _plantRepository.Get();
        }
    }
}
