using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace PracticaSQlite.clases
{
    internal class Constantes
    {
        public const string DatabaseFilename = "Libreria.db3";
        public const SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
