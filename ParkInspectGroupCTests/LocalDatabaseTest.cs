using System;
using System.IO;
using LocalDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkInspectGroupCTests
{
    [TestClass]
    public class LocalDatabaseTest
    {
        private readonly string fileName = "ParkInspectTest";

        [TestMethod]
        public void CreateSQLiteDatabaseTestNonExistingDB()
        {
            //Test if the database is created or not
            //arrange
            LocalDatabaseMain ldb;
            var exists = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            if (File.Exists(fileName + ".sqlite"))
                exists = true;

            //assert
            Assert.AreEqual(true, exists, "The database was not created or the file could not be found!");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void CreateSQLiteDatabaseTestDBExist()
        {
            //Test if the database is created or not
            //arrange
            LocalDatabaseMain ldb;
            var exists = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            if (File.Exists(fileName + ".sqlite"))
                exists = true;

            //assert
            Assert.AreEqual(true, exists, "The database was not created or the file could not be found!");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void CreateRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "INSERT INTO QuestionSort (Description, ExistsInCentral) values ('Test', '0')";
            var execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.CreateRecord(sql);

            //assert
            Assert.AreEqual(true, execute, "The record was not added to the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void UpdateRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "UPDATE QuestionSort SET Description = 'Another Test' WHERE Id = 1";
            var execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.UpdateRecord(sql);

            //assert
            Assert.AreEqual(true, execute, "The record was not updated in the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void GetRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "SELECT Id FROM QuestionSort WHERE Id = 1";

            //act
            ldb = new LocalDatabaseMain(fileName);
            var get = ldb.GetRecords(sql);
            var records = get.Rows.Count;

            //assert
            Assert.AreEqual(1, records, "The record was not found in the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void GetRecordTestFail()
        {
            //Test if you can add a record to the database that does not exist
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "SELECT Id FROM QuestionSortt WHERE Id = 5000"; //Non existing table

            //act
            ldb = new LocalDatabaseMain(fileName);
            var get = ldb.GetRecords(sql);
            var records = get.Rows.Count;

            //assert
            Assert.AreEqual(0, records, "The record was not found in the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void DeleteRecordTest()
        {
            //Test if you can delete a record from the database
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "DELETE FROM QuestionSort WHERE Id = 1";
            var execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.DeleteRecord(sql);

            //assert
            Assert.AreEqual(true, execute, "The record was not deleted from the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void DeleteRecordTestFail()
        {
            //Test if you can delete a record from the database taht does not exist
            //arrange
            LocalDatabaseMain ldb;
            var fileName = "ParkInspectTest";
            var sql = "DELETE FROM QuestionSortt WHERE Id = 1"; //Non existing table
            var execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.DeleteRecord(sql);

            //assert
            Assert.AreEqual(false, execute, "The record was not deleted from the database");
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void DeleteDatabase()
        {
            GC.Collect();
            File.Delete(fileName + ".sqlite");

            var exist = false;
            if (File.Exists(fileName + ".sqlite"))
                exist = true;

            Assert.AreEqual(false, exist, "Database was found!");
        }
    }
}