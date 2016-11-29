using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase;
using LocalDatabase.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System;

namespace ParkInspectGroupC.ViewModel
{
    public class DatabaseSyncViewModel : ViewModelBase
    {
        public ObservableCollection<SaveDeleteMessage> theSaveDeleteMessages { get; set; }
        public ObservableCollection<UpdateMessage> theUpdateMessages { get; set; }
        public ICommand LCSyncCommand { get; set; }
        public ICommand CLSyncCommand { get; set; }
        LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");
        public DatabaseSyncViewModel()
        {
            LCSyncCommand = new RelayCommand(LocalToCentralSync);
            CLSyncCommand = new RelayCommand(LocalToCentralSync);

            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>();
            theUpdateMessages = new ObservableCollection<UpdateMessage>();
        }

        private void LocalToCentralSync()
        {
            List<SaveDeleteMessage> SaveDeleteMessage = SaveDelete(ldb).Result;
            Tuple<List<UpdateMessage>, SaveDeleteMessage> UpdateMessages = Update(ldb).Result;
            List<UpdateMessage> UpdateMessage = UpdateMessages.Item1;

            foreach (SaveDeleteMessage m in SaveDeleteMessage)
            {
                theSaveDeleteMessages.Add(m);
            }

            theSaveDeleteMessages.Add(UpdateMessages.Item2);

            foreach (UpdateMessage m in UpdateMessage)
            {
                theUpdateMessages.Add(m);
            }
        }

        private static async Task<List<SaveDeleteMessage>> SaveDelete(LocalDatabaseMain ldb)
        {
            Debug.WriteLine("SaveDelete: Starting Sync");
            //List<SaveDeleteMessage> message = await Task.Run(() => ldb.SyncLocalToCentralSaveDelete());//Start the sync
            List<SaveDeleteMessage> message = ldb.SyncLocalToCentralSaveDelete();
            Debug.WriteLine("SaveDelete: Sync Done");
            Debug.WriteLine("SaveDelete: Sync Stopping");

            return message;
        }
        private static async Task<Tuple<List<UpdateMessage>, SaveDeleteMessage>> Update(LocalDatabaseMain ldb)
        {
            Debug.WriteLine("Update: Starting Sync");
            //List<SaveDeleteMessage> message = await Task.Run(() => ldb.SyncLocalToCentralSaveDelete());//Start the sync
            Tuple<List<UpdateMessage>,SaveDeleteMessage> UpdateMessages = ldb.SyncLocalToCentralUpdate(); ;
            Debug.WriteLine("Update: Sync Done");
            Debug.WriteLine("Update: Sync Stopping");

            return UpdateMessages;
        }
    }
}