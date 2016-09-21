using System;
using SQLite.Net;
using WhenToDig83.Data.Contracts;
using WhenToDig83.Droid;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DbConnection))]
namespace WhenToDig83.Droid
{
    public class DbConnection : ISQLite
    {
        private SQLiteConnectionWithLock _conn;

        public DbConnection()
        {
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