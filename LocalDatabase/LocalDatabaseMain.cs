using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using LocalDatabase.Central;
using LocalDatabase.Domain;
using LocalDatabase.Local;

namespace LocalDatabase
{
    public class LocalDatabaseMain
    {
        private readonly DatabaseActions _sqliteActions;
        public SQLiteConnection _sqliteConnection;
        private readonly string dbName;

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
            var db = false;
            if (File.Exists(dbName + ".sqlite"))
            {
                _sqliteConnection = new SQLiteConnection("Data Source=" + dbName + ".sqlite;Version=3;");
                db = true;
            }
            else
            {
                var createDB = new CreateSQLiteDatabase();
                db = createDB.Create(_sqliteActions, dbName, false);
                    //When true database is created and thus exists, when false creating database failed
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

        public async Task<bool> SyncCentralToLocal()
        {
            var refresh = Task.Run(() =>
                {
                    var sync = new GetFromCentral(_sqliteConnection, _sqliteActions);
                    var r = sync.Sync(dbName);

                    return r;
                }
            );

            var result = await refresh;
            return result;
        }

        public List<SaveDeleteMessage> SyncLocalToCentralSaveDelete()
        {
            var cont = true;
            var messages = new List<SaveDeleteMessage>();
            var syncSave = new SaveToCentral(_sqliteConnection, _sqliteActions);
            var syncDelete = new DeleteToCentral(_sqliteConnection, _sqliteActions);

            //Save
            var _newSave = new SaveDeleteMessage();
            _newSave.Action = "LtC Opslaan";
            _newSave.Date = DateTime.Now;
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
            var _newDelete = new SaveDeleteMessage();
            _newDelete.Action = "LtC Verwijderen";
            _newDelete.Date = DateTime.Now;
            var _temp = syncDelete.Save();
            if (_temp.Count == 0)
            {
                _newDelete.Message = "Succes!";
                messages.Add(_newDelete);
            }
            else
            {
                _newDelete.Message = "Er zijn meldingen:";
                messages.Add(_newDelete);
                foreach (var m in _temp)
                {
                    var _newTemp = new SaveDeleteMessage();
                    _newTemp.Message = m;
                    messages.Add(_newTemp);
                }
            }

            return messages;
        }

        public Tuple<List<UpdateMessage>, SaveDeleteMessage> SyncLocalToCentralUpdate()
        {
            var UpdateMessages = new List<UpdateMessage>();
            var UpdateSync = new UpdateToCentral(_sqliteConnection, _sqliteActions);

            //Update
            var _newUpdate = new SaveDeleteMessage();
            _newUpdate.Action = "LtC Updaten";
            _newUpdate.Date = DateTime.Now;
            var Update = UpdateSync.Update();

            if (Update.Item1)
            {
                _newUpdate.Message = "Succes! Er zijn " + Update.Item2.Count + " conflicten gevonden.";
                UpdateMessages = Update.Item2;
            }
            else
            {
                _newUpdate.Message = "Failed! Stoppen met synchroniseren.";
            }

            return Tuple.Create(UpdateMessages, _newUpdate);
        }

        #endregion

        #region Database Sync Update Actions

        public string GetLocalDetails(UpdateMessage message)
        {
            string msg = null;
            var glrd = new GetLocalRecordDetails();

            switch (message.LocalDatabaseName)
            {
                case "Region":
                    msg = glrd.GetRegion(message);
                    break;
                case "EmployeeStatus":
                    msg = glrd.GetEmployeeStatus(message);
                    break;
                case "Employee":
                    msg = glrd.GetEmployee(message);
                    break;
                case "Account":
                    msg = glrd.GetAccount(message);
                    break;
                case "Availability":
                    msg = glrd.GetAvailability(message);
                    break;
                case "WorkingHours":
                    msg = glrd.GetWorkingHours(message);
                    break;
                case "Customer":
                    msg = glrd.GetCustomer(message);
                    break;
                case "Assignment":
                    msg = glrd.GetAssignment(message);
                    break;
                case "InspectionStatus":
                    msg = glrd.GetInspectionStatus(message);
                    break;
                case "Inspection":
                    msg = glrd.GetInspection(message);
                    break;
                case "Coordinate":
                    msg = glrd.GetCoordinate(message);
                    break;
                case "InspectionImage":
                    msg = glrd.GetInspectionImage(message);
                    break;
                case "KeywordCategory":
                    msg = glrd.GetKeywordCategory(message);
                    break;
                case "Keyword":
                    msg = glrd.GetKeyword(message);
                    break;
                case "Module":
                    msg = glrd.GetModule(message);
                    break;
                case "QuestionSort":
                    msg = glrd.GetQuestionSort(message);
                    break;
                case "Question":
                    msg = glrd.GetQuestion(message);
                    break;
                case "Questionaire":
                    msg = glrd.GetQuestionaire(message);
                    break;
                case "QuestionAnswer":
                    msg = glrd.GetQuestionAnswer(message);
                    break;
                case "QuestionaireModule":
                    msg = glrd.GetQuestionaireModule(message);
                    break;
                case "QuestionKeyword":
                    msg = glrd.GetQuestionKeyword(message);
                    break;
            }

            return msg;
        }

        public string GetCentralDetails(UpdateMessage message)
        {
            string msg = null;
            var glrd = new GetCentralRecordDetails();

            switch (message.LocalDatabaseName)
            {
                case "Region":
                    msg = glrd.GetRegion(message);
                    break;
                case "EmployeeStatus":
                    msg = glrd.GetEmployeeStatus(message);
                    break;
                case "Employee":
                    msg = glrd.GetEmployee(message);
                    break;
                case "Account":
                    msg = glrd.GetAccount(message);
                    break;
                case "Availability":
                    msg = glrd.GetAvailability(message);
                    break;
                case "WorkingHours":
                    msg = glrd.GetWorkingHours(message);
                    break;
                case "Customer":
                    msg = glrd.GetCustomer(message);
                    break;
                case "Assignment":
                    msg = glrd.GetAssignment(message);
                    break;
                case "InspectionStatus":
                    msg = glrd.GetInspectionStatus(message);
                    break;
                case "Inspection":
                    msg = glrd.GetInspection(message);
                    break;
                case "Coordinate":
                    msg = glrd.GetCoordinate(message);
                    break;
                case "InspectionImage":
                    msg = glrd.GetInspectionImage(message);
                    break;
                case "KeywordCategory":
                    msg = glrd.GetKeywordCategory(message);
                    break;
                case "Keyword":
                    msg = glrd.GetKeyword(message);
                    break;
                case "Module":
                    msg = glrd.GetModule(message);
                    break;
                case "QuestionSort":
                    msg = glrd.GetQuestionSort(message);
                    break;
                case "Question":
                    msg = glrd.GetQuestion(message);
                    break;
                case "Questionaire":
                    msg = glrd.GetQuestionaire(message);
                    break;
                case "QuestionAnswer":
                    msg = glrd.GetQuestionAnswer(message);
                    break;
                case "QuestionaireModule":
                    msg = glrd.GetQuestionaireModule(message);
                    break;
                case "QuestionKeyword":
                    msg = glrd.GetQuestionKeyword(message);
                    break;
            }

            return msg;
        }

        public void KeepLocal(UpdateMessage message)
        {
            var kl = new KeepLocal();

            switch (message.LocalDatabaseName)
            {
                case "Region":
                    kl.Region(message);
                    break;
                case "EmployeeStatus":
                    kl.EmployeeStatus(message);
                    break;
                case "Employee":
                    kl.Employee(message);
                    break;
                case "Account":
                    kl.Account(message);
                    break;
                case "Availability":
                    kl.Availability(message);
                    break;
                case "WorkingHours":
                    kl.WorkingHours(message);
                    break;
                case "Customer":
                    kl.Customer(message);
                    break;
                case "Assignment":
                    kl.Assignment(message);
                    break;
                case "InspectionStatus":
                    kl.InspectionStatus(message);
                    break;
                case "Inspection":
                    kl.Inspection(message);
                    break;
                case "Coordinate":
                    kl.Coordinate(message);
                    break;
                case "InspectionImage":
                    kl.InspectionImage(message);
                    break;
                case "KeywordCategory":
                    kl.KeywordCategory(message);
                    break;
                case "Keyword":
                    kl.Keyword(message);
                    break;
                case "Module":
                    kl.Module(message);
                    break;
                case "QuestionSort":
                    kl.QuestionSort(message);
                    break;
                case "Question":
                    kl.Question(message);
                    break;
                case "Questionaire":
                    kl.Questionaire(message);
                    break;
                case "QuestionAnswer":
                    kl.QuestionAnswer(message);
                    break;
                case "QuestionaireModule":
                    kl.GetQuestionaireModule(message);
                    break;
                case "QuestionKeyword":
                    kl.QuestionKeyword(message);
                    break;
            }
        }

        #endregion
    }
}