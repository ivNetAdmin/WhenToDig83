using SQLite.Net.Attributes;
using System;
using WhenToDig83.Core.Helpers;

namespace WhenToDig83.Core.Entities
{
    public class Frost : DbKey
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Date { get; set; }      
    }
}
