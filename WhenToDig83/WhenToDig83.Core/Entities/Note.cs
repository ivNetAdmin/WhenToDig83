
namespace WhenToDig83.Core.Entities
{
    public class Note : DbKey
    {
        public string Notes { get; set; }
        public int TypeId { get; set; }
        public int Type { get; set; }
    }
}
