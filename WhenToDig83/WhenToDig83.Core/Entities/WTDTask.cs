using SQLite.Net.Attributes;
using System;

namespace WhenToDig83.Core.Entities
{
    public class WTDTask : DbKey
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public DateTime Date { get; set; }       
        [Ignore]
        public string TypeImage
        {
            get
            {
                switch (TypeId)
                {
                    case 1:
                        return "cultivate.png";
                    case 2:
                        return "plant.png";
                    default:
                        return "other.png";
                }
            }
        }
    }

}