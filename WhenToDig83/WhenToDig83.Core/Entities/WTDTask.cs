using System;

namespace WhenToDig83.Core.Entities
{
    public class WTDTask : DbKey
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }        
    }

}