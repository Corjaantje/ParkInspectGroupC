using System;
using System.Data.SQLite;

namespace LocalDatabase.Local
{
    public class CreateSQLiteDatabase
    {
        public bool Create(DatabaseActions _sqliteActions, string dbName, bool sync)
        {
            bool createdbfile = false;

            if (!sync)
            {
                //Create database file
                createdbfile = CreateDatabase(dbName);
            }

            if (createdbfile || sync)
            {
                //Create the connection & Create the tables
                SQLiteConnection _connection = new SQLiteConnection("Data Source=" + dbName + ".sqlite;Version=3;");
                bool createtable = false;

                createtable = Region(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = EmployeeStatus(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = Employee(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = Account(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = Availability(_sqliteActions, _connection);
                if (!createtable) return false;

                createdbfile = WorkingHours(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Customer(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Assignment(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = InspectionStatus(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Inspection(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Coordinate(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = InspectionImage(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = KeywordCategory(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Keyword(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Module(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionSort(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Question(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Questionaire(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionAnswer(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionaireModule(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionKeyword(_sqliteActions, _connection);

                return createdbfile;
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
            catch (Exception)
            {
                return false;
            }
        }

        #region Database tables
        private bool Account(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Account (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Username VARCHAR(25) NOT NULL," +
                "Password VARCHAR(50) NOT NULL DEFAULT 'parkinspect'," +
                "UserGuid VARCHAR(50) NOT NULL," +
                "EmployeeId INTEGER NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Region(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Region (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Region VARCHAR(50) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool EmployeeStatus(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE EmployeeStatus (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Description VARCHAR(25) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Employee(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Employee (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "FirstName VARCHAR(30) NOT NULL," +
                "Prefix VARCHAR(10) NULL," +
                "SurName VARCHAR(30) NOT NULL," +
                "Gender CHAR NULL," +
                "City VARCHAR(20) NULL, " +
                "Address VARCHAR(50) NULL, " +
                "ZipCode VARCHAR(10) NULL, " +
                "Phonenumber VARCHAR(15) NULL, " +
                "Email VARCHAR(50) NULL," +
                "RegionId INTEGER NULL," +
                "EmployeeStatusId INTEGER NULL DEFAULT 1," +
                "IsInspecter BIT NOT NULL, " +
                "IsManager BIT NOT NULL DEFAULT 0, " +
                "ManagerId INTEGER NULL, " +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(RegionId) REFERENCES Region(Id)," +
                "FOREIGN KEY(EmployeeStatusId) REFERENCES EmployeeStatus(Id)," +
                "FOREIGN KEY(ManagerId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Availability(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Availability (" +
                "EmployeeId INTEGER NOT NULL," +
                "Date DATE NOT NULL," +
                "StartTime TIME(0) NULL," +
                "EndTime TIME(0) NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(EmployeeId, Date)," +
                "FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool WorkingHours(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE WorkingHours (" +
                "EmployeeId INTEGER NOT NULL," +
                "Date DATE NOT NULL," +
                "StartTime TIME(0) NULL," +
                "EndTime TIME(0) NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(EmployeeId, Date)," +
                "FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Customer(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Customer (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Name VARCHAR(15) NOT NULL," +
                "Address VARCHAR(50) NULL," +
                "Location VARCHAR(50) NULL," +
                "Phonenumber VARCHAR(15) NULL," +
                "Email VARCHAR(50) NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Assignment(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Assignment (" +
                "Id INTEGER PRIMARY KEY NOT NULL, " +
                "CustomerId INTEGER NOT NULL, " +
                "ManagerId INTEGER NOT NULL, " +
                "Description VARCHAR(255) NULL," +
                "StartDate DATE NULL," +
                "EndDate DATE NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(ManagerId) REFERENCES Employee(Id)," +
                "FOREIGN KEY(CustomerId) REFERENCES Customer(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool InspectionStatus(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE InspectionStatus (" +
                "Id INTEGER PRIMARY KEY NOT NULL, " +
                "Description VARCHAR(50) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Inspection(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Inspection (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "AssignmentId INTEGER NOT NULL," +
                "RegionId INTEGER NOT NULL," +
                "Location VARCHAR(50) NOT NULL," +
                "StartDate DATETIME NOT NULL," +
                "EndDate DATETIME NOT NULL," +
                "StatusId INTEGER NOT NULL," +
                "InspectorId INTEGER NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(AssignmentId) REFERENCES Assignment(Id)," +
                "FOREIGN KEY(StatusId) REFERENCES InspectionStatus(Id)," +
                "FOREIGN KEY(RegionId) REFERENCES Region(Id)," +
                "FOREIGN KEY(InspectorId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Coordinate(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Coordinate (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Longitude FLOAT NOT NULL," +
                "Latitude FLOAT NOT NULL," +
                "Note VARCHAR(255) NULL," +
                "InspectionId INTEGER NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(InspectionId) REFERENCES Inspection(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool InspectionImage(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE InspectionImage (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "File VARCHAR(255) NOT NULL," +
                "InspectionId INTEGER NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(InspectionId) REFERENCES Inspection(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Module(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Module (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Name VARCHAR(50) NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "Note VARCHAR(255) NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool QuestionSort(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE QuestionSort (" +
                "Id INTEGER PRIMARY KEY NOT NULL, " +
                "Description VARCHAR(255) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Question(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Question (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "SortId INTEGER NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "ModuleId INTEGER NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(ModuleId) REFERENCES Module(Id)," +
                "FOREIGN KEY(SortId) REFERENCES QuestionSort(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool QuestionAnswer(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE QuestionAnswer (" +
                "QuestionnaireId INTEGER NOT NULL ," +
                "QuestionId INTEGER NOT NULL," +
                "Result VARCHAR(255) NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(QuestionId, QuestionnaireId)," +
                "FOREIGN KEY(QuestionnaireId) REFERENCES Questionnaire(Id)," +
                "FOREIGN KEY(QuestionId) REFERENCES Question(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Questionaire(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Questionaire (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "InspectionId INTEGER NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(InspectionId) REFERENCES Inspection(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool QuestionaireModule(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE QuestionaireModule (" +
                "ModuleId INTEGER NOT NULL," +
                "QuestionaireId INTEGER NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(ModuleId, QuestionaireId)," +
                "FOREIGN KEY(ModuleId) REFERENCES Module(Id)," +
                "FOREIGN KEY(QuestionaireId) REFERENCES Questionaire(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool QuestionKeyword(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE QuestionKeyword (" +
                "QuestionId INTEGER NOT NULL, " +
                "KeywordId INTEGER NOT NULL, " +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(KeywordId, QuestionId)," +
                "FOREIGN KEY(QuestionId) REFERENCES Question(Id)," +
                "FOREIGN KEY(KeywordId) REFERENCES Keyword(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Keyword(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Keyword (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "CategoryId INTEGER NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(CategoryId) REFERENCES KeywordCategory(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool KeywordCategory(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE KeywordCategory (" +
                "Id INTEGER PRIMARY KEY NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "DateCreated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "DateUpdated datetime DEFAULT CURRENT_TIMESTAMP NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        #endregion
    }
}