using LocalDatabase.Local;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace LocalDatabase
{
    public class LocalDatabaseMain
    {
        private string dbName;
        SQLiteConnection _sqliteConnection;
        DatabaseActions _sqliteActions;

        public LocalDatabaseMain(string _dbName)
        {
            dbName = _dbName;
            _sqliteActions = new DatabaseActions();
            CheckForAndCreateDatabase();
        }
        public bool CheckForAndCreateDatabase()
        {
            //Check if database exists, if not create the database
            //Set up the connection
            bool db = false;
            if (File.Exists(dbName + ".sqlite"))
            {
                _sqliteConnection = new SQLiteConnection("Data Source=" + dbName + ".sqlite;Version=3;");
                db = true;
            }
            else
            {
                CreateSQLiteDatabase createDB = new CreateSQLiteDatabase();
                db = createDB.Create(_sqliteActions, dbName); //When true database is created and thus exists, when false creating database failed
                _sqliteConnection = new SQLiteConnection("Data Source=" + dbName + ".sqlite;Version=3;");
            }

            return db;
        }

        #region Database Actions
        public bool CreateRecord(string sql)
        {
            return _sqliteActions.CUD(_sqliteConnection, sql);
        }
        public bool UpdateRecord(string sql)
        {
            return _sqliteActions.CUD(_sqliteConnection, sql);
        }
        public bool DeleteRecord(string sql)
        {
            return _sqliteActions.CUD(_sqliteConnection, sql);
        }
        public DataTable GetRecords(string sql)
        {
            return _sqliteActions.Get(_sqliteConnection, sql);
        }
        #endregion

        #region Database Sync Options
        public string SyncCentralToLocal()//TODO
        {
            return "";
        }
        
        /*public Tuple<string, List<Record>, List<Record>> SyncLocalToCentral()//TODO
        {
            //The SQL database needs te be more finished for this part because we need the Entity Model for this part
            //Could do without but we do need to use Entity Framework
            return "";
        }*/
        #endregion
    }
}
