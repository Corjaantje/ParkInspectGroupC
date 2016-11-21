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

                createtable = Region(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = EmployeeStatus(_sqliteActions, _connection);
                if (!createtable) return false;

                createtable = Employee(_sqliteActions, _connection);
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

                createdbfile = Module(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionSort(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = Question(_sqliteActions, _connection);
                if (!createdbfile) return false;

                createdbfile = QuestionAnswer(_sqliteActions, _connection);

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
                "Id INT PRIMARY KEY," +
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
        private bool Region(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Region (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "Region VARCHAR(50) NOT NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool EmployeeStatus(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE EmployeeStatus (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "Description VARCHAR(25) NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Employee(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Employee (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "FirstName VARCHAR(30) NOT NULL," +
                "Prefix VARCHAR(10) NULL," +
                "SurName VARCHAR(30) NOT NULL," +
                "Gender CHAR NULL," +
                "City VARCHAR(20) NULL, " +
                "Address VARCHAR(50) NULL, " +
                "ZipCode VARCHAR(10) NULL, " +
                "Phonenumber VARCHAR(15) NULL, " +
                "Email VARCHAR(50) NULL," +
                "RegionId INT NULL," +
                "EmployeeStatusId INT NULL," +
                "Inspecter BIT NOT NULL, " +
                "Manager BIT NOT NULL, " +
                "AccountId INT NOT NULL, " +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(RegionId) REFERENCES Region(Id)," +
                "FOREIGN KEY(EmployeeStatusId) REFERENCES EmployeeStatus(Id)," +
                "FOREIGN KEY(AccountId) REFERENCES Account(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Availability(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Availability (" +
                "EmployeeId INT," +
                "Date DATE NULL," +
                "EndTime TIME(0) NULL,"+ 
                "StartTime TIME(0) NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool WorkingHours(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE WorkingHours (" +
                "EmployeeId INT," +
                "Date DATE NULL," +
                "StartTime TIME(0) NULL," +
                "EndTime TIME(0) NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Customer(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Customer (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "AccountId INT NOT NULL," +
                "Name VARCHAR(15) NOT NULL," +
                "Address VARCHAR(50) NULL," +
                "Location VARCHAR(50) NULL," +
                "Phonenumber VARCHAR(15) NULL," +
                "Email VARCHAR(50) NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(AccountId) REFERENCES Account(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Assignment(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Assignment (" +
                "Id INT NOT NULL PRIMARY KEY, " +
                "CustomerId INT NOT NULL, " +
                "ManagerId INT NOT NULL, " +
                "Description VARCHAR(255) NULL," +
                "StartDate DATE NULL," +
                "EndDate DATE NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
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
                "Id INT NOT NULL PRIMARY KEY, " +
                "Description VARCHAR(50) NOT NULL " +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Inspection(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Inspection (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "AssignmentId INT NOT NULL," +
                "RegionId INT NOT NULL," +
                "Location VARCHAR(50) NOT NULL," +
                "StartDate DATETIME NOT NULL," +
                "EndDate DATETIME NOT NULL," +
                "StatusId INT NOT NULL," +
                "InspectorId INT NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "FOREIGN KEY(AssignmentId) REFERENCES Assignment(Id)," +
                "FOREIGN KEY(StatusId) REFERENCES InspectionStatus(Id)," +
                "FOREIGN KEY(RegionId) REFERENCES Region(Id)," +
                "FOREIGN KEY(InspectorId) REFERENCES Employee(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Module(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Module (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "Name VARCHAR(50) NOT NULL," +
                "Description VARCHAR(255) NOT NULL" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool QuestionSort(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE QuestionSort (" +
                "Id INT NOT NULL PRIMARY KEY, " +
                "Description VARCHAR(255) NOT NULL " +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        private bool Question(DatabaseActions _sqliteActions, SQLiteConnection _connection)
        {
            string sql = "CREATE TABLE Question (" +
                "Id INT NOT NULL PRIMARY KEY," +
                "SortId INT NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "ModuleId INT NULL," +
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
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
                "QuestionnaireId INT NOT NULL ,"+
                "QuestionId INT NOT NULL,"+
                "Result VARCHAR(255) NULL,"+
                "DateCreated datetime NOT NULL," +
                "DateUpdated datetime NOT NULL," +
                "ExistsInCentral INT NOT NULL," +
                "PRIMARY KEY(QuestionId, QuestionnaireId)," +
                "FOREIGN KEY(QuestionnaireId) REFERENCES Questionnaire(Id)," +
                "FOREIGN KEY(QuestionId) REFERENCES Question(Id)" +
                ")";

            bool action = _sqliteActions.CUD(_connection, sql);
            return action;
        }
        #endregion
    }
}