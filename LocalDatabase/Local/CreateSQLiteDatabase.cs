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
        #endregion
    }
}