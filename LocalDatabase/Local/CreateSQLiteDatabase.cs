using System;
using System.Data.SQLite;

namespace LocalDatabase.Local
{
    public class CreateSQLiteDatabase
    {
        DatabaseActions _sqliteActions;
        public bool Create(DatabaseActions _sqliteActions, string dbName)
        {
            bool createdbfile = false;

            //Create database file
            createdbfile = CreateDatabase(dbName);

            if (createdbfile)
            {
                //Create the connection & Create the tables
                SQLiteConnection _connection = new SQLiteConnection("Data Source=" + dbName + ".sqlite;Version=3;");
                bool createtable = false;

                createtable = Account(_sqliteActions, _connection);
                if (!createtable) return false;

                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CreateDatabase(string dbName)
        {
            try
            {
                SQLiteConnection.CreateFile(dbName + ".sqlite");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region Database tables
        private bool Account(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Account (" +
                "Id INTEGER PRIMARY KEY," +
                "Username VARCHAR(25) NOT NULL," +
                "Password VARCHAR(25) NOT NULL," +
                "UserGuid VARCHAR(50) NOT NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        #endregion
    }
}