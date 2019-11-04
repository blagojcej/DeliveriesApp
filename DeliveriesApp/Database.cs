using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = System.Environment;

namespace DeliveriesApp
{
    public static class Database
    {
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
    }
}