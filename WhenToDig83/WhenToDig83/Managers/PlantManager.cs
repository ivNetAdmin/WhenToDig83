
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
            _varietyRepository = new RepositoryAsync<Variety>();
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

        public async Task<Plant> GetPlant(int plantId)
        {
            return await _plantRepository.Get(plantId);
        }

        internal async void AddVariety(string name, string notes, int plantId, int varietyId)
        {
            if (varietyId == 0)
            {
                var variety = new Variety { Name = name, PlantId = plantId };
                await _varietyRepository.Insert(variety);
                await _noteRepository.Insert(new Note { Type = (int)NoteType.Variety, TypeId = variety.ID, Notes = notes });
            }
            else
            {
                var variety = await _varietyRepository.Get(varietyId);
                variety.Name = name;
                await _varietyRepository.Update(variety);

                var note = await _noteRepository.Get(predicate: x => x.Type == (int)NoteType.Variety && x.TypeId == varietyId);

                if (note == null)
                {
                    await _noteRepository.Insert(new Note { Type = (int)NoteType.Variety, TypeId = varietyId, Notes = notes });
                }
                else
                {
                    note.Type = (int)NoteType.Variety;
                    note.TypeId = varietyId;
                    note.Notes = notes;
                    await _noteRepository.Update(note);
                }
            }
        }
    }
}
