using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalDatabase;
using System.IO;
using System.Data;

namespace ParkInspectGroupCTests
{
    [TestClass]
    public class LocalDatabaseTest
    {
        [TestMethod]
        public void CreateSQLiteDatabaseTest()
        {
            //Test if the database is created or not
            //arrange
            LocalDatabaseMain ldb;
            bool exists = false;
            string fileName = "ParkInspectTest";

            //act
            ldb = new LocalDatabaseMain(fileName);
            if (File.Exists(fileName + ".sqlite"))
            {
                exists = true;
            }

            //assert
            Assert.AreEqual(true, exists, string.Format("The database was not created or the file could not be found!"));
        }

        [TestMethod]
        public void CreateRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            string fileName = "ParkInspectTest";
            string sql = "INSERT INTO QuestionSort (Description, ExistsInCentral) values ('Test', '0')";
            bool execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.CreateRecord(sql);

            //assert
            Assert.AreEqual(true, execute, string.Format("The record was not added to the database"));
        }

        [TestMethod]
        public void UpdateRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            string fileName = "ParkInspectTest";
            string sql = "UPDATE QuestionSort SET Description = 'Another Test' WHERE Id = 1";
            bool execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.UpdateRecord(sql);

            //assert
            Assert.AreEqual(true, execute, string.Format("The record was not updated in the database"));
        }

        [TestMethod]
        public void GetRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            string fileName = "ParkInspectTest";
            string sql = "SELECT Id FROM QuestionSort WHERE Id = 1";

            //act
            ldb = new LocalDatabaseMain(fileName);
            DataTable get = ldb.GetRecords(sql);
            int records = get.Rows.Count;

            //assert
            Assert.AreEqual(1, records, string.Format("The record was not found in the database"));
        }

        [TestMethod]
        public void DeleteRecordTest()
        {
            //Test if you can add a record to the database
            //arrange
            LocalDatabaseMain ldb;
            string fileName = "ParkInspectTest";
            string sql = "DELETE FROM QuestionSort WHERE Id = 1";
            bool execute = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            execute = ldb.DeleteRecord(sql);

            //assert
            Assert.AreEqual(true, execute, string.Format("The record was not deleted from the database"));
        }
    }
}