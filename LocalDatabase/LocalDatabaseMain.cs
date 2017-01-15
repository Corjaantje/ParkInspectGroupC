using LocalDatabase.Central;
using LocalDatabase.Domain;
using LocalDatabase.Local;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace LocalDatabase
{
    public class LocalDatabaseMain
    {
        private string dbName;
        public SQLiteConnection _sqliteConnection;
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
        public async Task<bool> SyncCentralToLocal()
        {
            Task<bool> refresh = Task.Run(() =>
            {
                GetFromCentral sync = new GetFromCentral(_sqliteConnection, _sqliteActions);
                bool r = sync.Sync(dbName);

                return r;
            }
            );

            bool result = await refresh;
            return result;
        }
        public List<SaveDeleteMessage> SyncLocalToCentralSaveDelete()
        {
            bool cont = true;
            List<SaveDeleteMessage> messages = new List<SaveDeleteMessage>();
            SaveToCentral syncSave = new SaveToCentral(_sqliteConnection, _sqliteActions);
            DeleteToCentral syncDelete = new DeleteToCentral(_sqliteConnection, _sqliteActions);

            //Save
            SaveDeleteMessage _newSave = new SaveDeleteMessage();
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
            SaveDeleteMessage _newDelete = new SaveDeleteMessage();
            _newDelete.Action = "LtC Verwijderen";
            _newDelete.Date = DateTime.Now;
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
        public Tuple<List<UpdateMessage>,SaveDeleteMessage> SyncLocalToCentralUpdate()
        {
            List<UpdateMessage> UpdateMessages = new List<UpdateMessage>();
            UpdateToCentral UpdateSync = new UpdateToCentral(_sqliteConnection, _sqliteActions);

            //Update
            SaveDeleteMessage _newUpdate = new SaveDeleteMessage();
            _newUpdate.Action = "LtC Updaten";
            _newUpdate.Date = DateTime.Now;
            Tuple<bool, List<UpdateMessage>> Update = UpdateSync.Update();

            if (Update.Item1)
            {
                _newUpdate.Message = "Succes! Er zijn "+ Update.Item2.Count + " conflicten gevonden.";
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
            GetLocalRecordDetails glrd = new GetLocalRecordDetails();

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
            GetCentralRecordDetails glrd = new GetCentralRecordDetails();

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
            KeepLocal kl = new KeepLocal();

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
