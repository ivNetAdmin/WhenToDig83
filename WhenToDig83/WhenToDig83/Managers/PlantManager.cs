
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
        private RepositoryAsync<Variety> _varietyRepository;
        private RepositoryAsync<Note> _noteRepository;

        public PlantManager()
        {
            _plantRepository = new RepositoryAsync<Plant>();
            _noteRepository = new RepositoryAsync<Note>();
        }
        
        public async Task<List<Plant>> GetPlants()
        { 
            return await _plantRepository.Get();
        }

        public async Task<List<Variety>> GetVarieties(int plantId)
        {
            return await _varietyRepository.Get(predicate: x => x.PlantId == plantId, orderBy: x => x.Name);
        }

        internal async void AddPlant(string name, string notes, int plantId)
        {
            if (plantId == 0)
            {
                var plant = new Plant { Name = name };
                await _plantRepository.Insert(plant);
                await _noteRepository.Insert(new Note { Type = (int)NoteType.Plant, TypeId = plant.ID, Notes = notes });
            }
            else
            {
                var plant = await _plantRepository.Get(plantId);
                plant.Name = name;
                await _plantRepository.Update(plant);

                var note = await _noteRepository.Get(predicate: x => x.Type == (int)NoteType.Plant && x.TypeId == plantId);

                if (note == null)
                {
                    await _noteRepository.Insert(new Note { Type = (int)NoteType.Plant, TypeId = plantId, Notes = notes });
                }
                else
                {
                    note.Type = (int)NoteType.Plant;
                    note.TypeId = plantId;
                    note.Notes = notes;
                    await _noteRepository.Update(note);
                }
            }
        }
    }
}
