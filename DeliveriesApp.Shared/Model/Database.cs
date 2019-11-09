using SQLite;
using System;
using System.IO;

namespace DeliveriesApp.Shared.Model
{
    public static class Database
    {
        #region Properties

        public static SQLiteConnection Connection
        {
            get
            {
                try
                {
                    //Create database if not exists
                    string dbName = "deliveries.sqlite";
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string fullPath = Path.Combine(folderPath, dbName);

                    return new SQLiteConnection(fullPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        #endregion Properties

        #region Methods

        public static bool Insert<T>(T item)
        {
            try
            {
                Connection.CreateTable<T>();
                Connection.Insert(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        #endregion Methods
    }
}
