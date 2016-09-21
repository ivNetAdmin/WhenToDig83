using SQLite.Net.Attributes;
using WhenToDig83.Core.Contracts;

namespace WhenToDig83.Core.Entities
{
    public abstract class DbKey : IEntity
    {
        public DbKey() { }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}