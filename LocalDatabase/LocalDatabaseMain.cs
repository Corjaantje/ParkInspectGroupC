using LocalDatabase.Central;
using LocalDatabase.Domain;
using LocalDatabase.Local;
using System.Collections.Generic;
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
                db = createDB.Create(_sqliteActions, dbName, false); //When true database is created and thus exists, when false creating database failed
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
        public bool SyncCentralToLocal()
        {
            GetFromCentral sync = new GetFromCentral(_sqliteConnection, _sqliteActions);
            return sync.Sync(dbName);
        }
        public List<SaveDeleteMessage> SyncLocalToCentralSaveDelete()
        {
            bool cont = true;
            List<SaveDeleteMessage> messages = new List<SaveDeleteMessage>();
            SaveToCentral syncSave = new SaveToCentral(_sqliteConnection, _sqliteActions);
            DeleteToCentral syncDelete = new DeleteToCentral(_sqliteConnection, _sqliteActions);

            //Save
            SaveDeleteMessage _newSave = new SaveDeleteMessage();
            _newSave.Action = "Opslaan";
            if (syncSave.Save())
            {
                _newSave.Message = "Succes!";
            }
            else
            {
                _newSave.Message = "Failed! Stoppen met synchroniseren.";
                cont = false;
            }
            messages.Add(_newSave);

            if (!cont) return messages;

            //Delete
            SaveDeleteMessage _newDelete = new SaveDeleteMessage();
            _newSave.Action = "Verwijderen";
            List<string> _temp = syncDelete.Save();
            if (_temp.Count == 0)
            {
                _newDelete.Message = "Succes!";
                messages.Add(_newDelete);
            }
            else
            {
                _newDelete.Message = "Er zijn meldingen:";
                messages.Add(_newDelete);
                foreach (string m in _temp)
                {
                    SaveDeleteMessage _newTemp = new SaveDeleteMessage();
                    _newTemp.Message = m;
                    messages.Add(_newTemp);
                }
            }

            return messages;
        }
        #endregion
    }
}
