using SQLite.Net;

namespace WhenToDig83.Data.Contracts
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
