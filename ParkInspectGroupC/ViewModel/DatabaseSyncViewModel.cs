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
        private async void CentralToLocalSync()
        {
            SaveDeleteMessage _message = new SaveDeleteMessage();
            _message.Action = "CtL Synchroniseren";

            Task task = Task.Run(() => 
            {
                if (theUpdateMessages.Count == 0)
                {
                    _message.Date = DateTime.Now;
                    if (Properties.Settings.Default.OnOffline)
                    {
                        //Start sync with central
                        Task<bool> syncCToL = ldb.SyncCentralToLocal();
                        
                        if (syncCToL.Result)
                        {
                            _message.Message = "Succes!";
                        }
                        else
                        {
                            _message.Message = "Failed!";
                        }
                    }
                    else
                    {
                        _message.Message = "Kon niet gaan synchroniseren want er is geen verbinding met de centrale database!";
                    }
                }
            });

            await task;
            theSaveDeleteMessages.Add(_message);
        }
        #endregion

        #region Local to Central Sync
        private async void LocalToCentralSync()
        {
            List<SaveDeleteMessage> SaveDeleteMessage = null;
            Tuple<List<UpdateMessage>, SaveDeleteMessage> UpdateMessages = null;
            List<UpdateMessage> UpdateMessage = null;

            Task task = Task.Run(() =>
            {
                SaveDeleteMessage = SaveDelete(ldb).Result;
                UpdateMessages = Update(ldb).Result;
                UpdateMessage = UpdateMessages.Item1;
            });

            await task;

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
            List<SaveDeleteMessage> message = null;

            Task task = Task.Run(() =>
            {
                message = ldb.SyncLocalToCentralSaveDelete();
            });

            await task;

            return message;
        }
        private static async Task<Tuple<List<UpdateMessage>, SaveDeleteMessage>> Update(LocalDatabaseMain ldb)
        {
            Tuple<List<UpdateMessage>, SaveDeleteMessage> UpdateMessages = null;

            Task task = Task.Run(() =>
            {
                UpdateMessages = ldb.SyncLocalToCentralUpdate();
            });

            await task;

            return UpdateMessages;
        }
        #endregion
    }
}