using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase;
using LocalDatabase.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;

namespace ParkInspectGroupC.ViewModel
{
    public class DatabaseSyncViewModel : ViewModelBase
    {
        public ObservableCollection<SaveDeleteMessage> theSaveDeleteMessages { get; set; }
        public ICommand LCSyncCommand { get; set; }
        public ICommand CLSyncCommand { get; set; }
        public DatabaseSyncViewModel()
        {
            LCSyncCommand = new RelayCommand(LocalToCentralSync);
            CLSyncCommand = new RelayCommand(LocalToCentralSync);

            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>();
        }

        private void LocalToCentralSync()
        {
            List<SaveDeleteMessage> message = SaveDelete().Result;
            foreach (SaveDeleteMessage m in message)
            {
                theSaveDeleteMessages.Add(m);
            }
        }

        private static async Task<List<SaveDeleteMessage>> SaveDelete()
        {
            Debug.WriteLine("Starting Sync");
            LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");//Make the connection with the db
            //List<SaveDeleteMessage> message = await Task.Run(() => ldb.SyncLocalToCentralSaveDelete());//Start the sync
            List<SaveDeleteMessage> message = ldb.SyncLocalToCentralSaveDelete();
            Debug.WriteLine("Sync Done");
            Debug.WriteLine("Sync Stopping");

            return message;
        }
    }
}