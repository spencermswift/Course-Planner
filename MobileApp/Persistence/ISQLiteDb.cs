using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
