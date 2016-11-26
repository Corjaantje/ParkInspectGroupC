using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase;
using LocalDatabase.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

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

            LocalToCentralSync();
        }

        private void LocalToCentralSync()
        {
            List<SaveDeleteMessage> message = SaveDelete().Result;
            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>(message);
        }

        private static async Task<List<SaveDeleteMessage>> SaveDelete()
        {
            LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");//Make the connection with the db
            List<SaveDeleteMessage> message = await Task.Run(() => ldb.SyncLocalToCentralSaveDelete());//Start the sync

            return message;
        }
    }
}