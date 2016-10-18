using SQLite.Net.Attributes;

namespace WhenToDig83.Core.Entities
{
    public class Plant : DbKey
    {
        public string Name { get; set; }

        [Ignore]
        public string PlantImage { get { return "ic_plant_white_48dp.png"; } }
    }
}
