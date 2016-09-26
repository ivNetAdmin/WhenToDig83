using System;

namespace WhenToDig83.Core.Entities
{
    public class WTDTask : DbKey
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }

}