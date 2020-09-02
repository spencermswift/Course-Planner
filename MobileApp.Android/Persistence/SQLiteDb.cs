using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid;
using Xamarin.Forms;
using System.IO;
using Android.Provider;

[assembly: Dependency(typeof(SQLiteDb))]
namespace MobileApp.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docPath, "MySqlLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}