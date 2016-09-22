using SQLite.Net;
using SQLite.Net.Async;

namespace WhenToDig83.Data.Contracts
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
