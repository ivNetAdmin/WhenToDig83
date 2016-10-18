
using SQLite.Net.Attributes;
using WhenToDig83.Core.Helpers;

namespace WhenToDig83.Core.Entities
{
    public class Variety : DbKey
    {
        public string Name { get; set; }
        public int PlantId { get; set; }

        [Ignore]
        public string VarietyImage { get { return ImageHelper.Variety(); } }
    }
}
