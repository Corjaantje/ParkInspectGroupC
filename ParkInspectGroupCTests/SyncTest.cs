using System;
using System.IO;
using LocalDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkInspectGroupCTests
{
    [TestClass]
    public class SyncTest
    {
        private readonly string fileName = "ParkInspectTestSync";

        [TestMethod]
        public void CreateSQLiteDatabaseTest()
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
        public void CentralToLocalSyncTest()
        {
            //Arrange
            LocalDatabaseMain ldb;

            //Act
            ldb = new LocalDatabaseMain(fileName);
            var sync = ldb.SyncCentralToLocal();
            var result = sync.Result;

            //Assert
            Assert.AreEqual(true, result);
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void LocalToCentralSyncSaveAndDeleteTest()
        {
            //Arrange
            LocalDatabaseMain ldb;

            //Act
            ldb = new LocalDatabaseMain(fileName);
            var sync = ldb.SyncLocalToCentralSaveDelete();
            var list = sync.Count;

            //Assert
            Assert.AreEqual(2, list);
            ldb._sqliteConnection.Close();
            GC.Collect();
        }

        [TestMethod]
        public void LocalToCentralSyncUpdateTest()
        {
            //Arrange
            LocalDatabaseMain ldb;

            //Act
            ldb = new LocalDatabaseMain(fileName);
            var sync = ldb.SyncLocalToCentralUpdate();
            var list = sync.Item1.Count;

            //Assert
            Assert.AreEqual(0, list);
            ldb._sqliteConnection.Close();
            GC.Collect();
        }
    }
}