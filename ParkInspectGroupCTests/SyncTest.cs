using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalDatabase;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using LocalDatabase.Domain;

namespace ParkInspectGroupCTests
{
    [TestClass]
    public class SyncTest
    {
        string fileName = "ParkInspectTestSync";
        [TestMethod]
        public void CreateSQLiteDatabaseTest()
        {
            //Test if the database is created or not
            //arrange
            LocalDatabaseMain ldb;
            bool exists = false;

            //act
            ldb = new LocalDatabaseMain(fileName);
            if (File.Exists(fileName + ".sqlite"))
            {
                exists = true;
            }

            //assert
            Assert.AreEqual(true, exists, string.Format("The database was not created or the file could not be found!"));
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
            Task<bool> sync = ldb.SyncCentralToLocal();
            bool result = sync.Result;

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
            List<SaveDeleteMessage> sync = ldb.SyncLocalToCentralSaveDelete();
            int list = sync.Count;

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
            Tuple<List<UpdateMessage>, SaveDeleteMessage> sync = ldb.SyncLocalToCentralUpdate();
            int list = sync.Item1.Count;

            //Assert
            Assert.AreEqual(0,list);
            ldb._sqliteConnection.Close();
            GC.Collect();
        }
    }
}
