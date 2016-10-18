
using SQLite.Net.Attributes;
using WhenToDig83.Core.Helpers;

namespace WhenToDig83.Core.Entities
{
    public class Note : DbKey
    {
        public string Notes { get; set; }
        public string Meta { get; set; }
        public int TypeId { get; set; }
        public int Type { get; set; }
        [Ignore]
        public string TypeImage
        {
            get
            {
                switch (Type)
                {
                    case 1:
                        return "ic_task_white_48dp.png";
                    case 2:
                        return "ic_plant_white_48dp.png";
                    default:
                        return ImageHelper.Variety();
                }
            }
        }
    }
}
