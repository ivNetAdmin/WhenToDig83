using System;
using SQLite.Net;
using WhenToDig83.Data.Contracts;
using WhenToDig83.Droid;
using System.IO;
using SQLite.Net.Async;

[assembly: Xamarin.Forms.Dependency(typeof(DbConnection))]
namespace WhenToDig83.Droid
{
    public class DbConnection : ISQLite
    {
        private SQLiteConnectionWithLock _conn;

        public DbConnection()
        {
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var dbpath = GetDatabasePath();

            var platForm = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            var connectionFactory = new Func<SQLiteConnectionWithLock>(
                () =>
                {
                    if (_conn == null)
                    {
                        _conn =
                            new SQLiteConnectionWithLock(platForm,
                                new SQLiteConnectionString(dbpath, storeDateTimeAsTicks: false));
                    }
                    return _conn;
                });

            return new SQLiteAsyncConnection(connectionFactory);
        }

        public SQLiteConnection GetConnection()
        {
            var dbPath = GetDatabasePath();
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            return new SQLiteConnection(platform, dbPath);
        }

        private string GetDatabasePath()
        {
            var sqliteFilename = "WTD.db3";
            string documentsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}