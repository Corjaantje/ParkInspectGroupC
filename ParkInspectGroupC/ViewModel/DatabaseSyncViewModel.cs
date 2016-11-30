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
using ParkInspectGroupC.View.DatabaseSyncDialogs;
using System.Linq;

namespace ParkInspectGroupC.ViewModel
{
    public class DatabaseSyncViewModel : ViewModelBase
    {
        LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");
        public ObservableCollection<SaveDeleteMessage> theSaveDeleteMessages { get; set; }
        public ObservableCollection<UpdateMessage> theUpdateMessages { get; set; }
        public ICommand LCSyncCommand { get; set; }
        public ICommand CLSyncCommand { get; set; }
        public ICommand ShowUpdateMessage { get; set; }
        public ICommand DeleteUpdateMessage { get; set; }
        private UpdateMessage _selectedUpdateMessage;
        public UpdateMessage SelectedUpdateMessage
        {
            get { return _selectedUpdateMessage; }
            set { _selectedUpdateMessage = value; }
        }
        
        public DatabaseSyncViewModel()
        {
            LCSyncCommand = new RelayCommand(LocalToCentralSync);
            CLSyncCommand = new RelayCommand(CentralToLocalSync);
            ShowUpdateMessage = new RelayCommand(ShowUpdateMessageDialog);
            DeleteUpdateMessage = new RelayCommand(DeleteUpdateMessageDialog);

            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>();
            theUpdateMessages = new ObservableCollection<UpdateMessage>();
        }

        #region Conflict Buttons
        private void ShowUpdateMessageDialog()
        {
            if (SelectedUpdateMessage != null)
            {
                UpdateConflictDialog window = new UpdateConflictDialog(SelectedUpdateMessage, ldb);
                window.ShowDialog();

                if (window.DialogResult.HasValue && window.DialogResult.Value)
                {
                    theUpdateMessages.Remove(theUpdateMessages.Where(i => i.LocalDatabaseName == SelectedUpdateMessage.LocalDatabaseName)
                        .Where(i => i.LocalId == SelectedUpdateMessage.LocalId)
                        .Single());
                }
                CentralToLocalSync();
            }
        }
        private void DeleteUpdateMessageDialog()
        {
            if (SelectedUpdateMessage != null)
            {
                DeleteDialog window = new DeleteDialog();
                window.ShowDialog();

                if (window.DialogResult.HasValue && window.DialogResult.Value)
                {
                    theUpdateMessages.Remove(theUpdateMessages.Where(i => i.LocalDatabaseName == SelectedUpdateMessage.LocalDatabaseName)
                        .Where(i => i.LocalId == SelectedUpdateMessage.LocalId)
                        .Single());
                }
                CentralToLocalSync();
            }
        }
        #endregion

        #region Central to Local Sync
        private void CentralToLocalSync()
        {
            if (theUpdateMessages.Count == 0)
            {
                //Start sync with central
                Debug.WriteLine("CentralToLocal: Starting Sync");
                bool syncCToL = ldb.SyncCentralToLocal();

                SaveDeleteMessage _message = new SaveDeleteMessage();
                _message.Action = "CtL Synchroniseren";
                _message.Date = DateTime.Now;
                if (syncCToL)
                {
                    _message.Message = "Succes!";
                }
                else
                {
                    _message.Message = "Failed!";
                }
                theSaveDeleteMessages.Add(_message);
                Debug.WriteLine("CentralToLocal: Done Sync");
                Debug.WriteLine("CentralToLocal: Stopping Sync");
            }
        }
        #endregion

        #region Local to Central Sync
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

            CentralToLocalSync();
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
        #endregion
    }
}