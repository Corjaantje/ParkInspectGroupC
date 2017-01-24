using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase;
using LocalDatabase.Domain;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View.DatabaseSyncDialogs;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class DatabaseSyncViewModel : ViewModelBase
    {
        private readonly LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");

        public DatabaseSyncViewModel()
        {
            BackCommand = new RelayCommand(PerformBack);
            LCSyncCommand = new RelayCommand(LocalToCentralSync);
            CLSyncCommand = new RelayCommand(CentralToLocalSync);
            ShowUpdateMessage = new RelayCommand(ShowUpdateMessageDialog);
            DeleteUpdateMessage = new RelayCommand(DeleteUpdateMessageDialog);

            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>();
            theUpdateMessages = new ObservableCollection<UpdateMessage>();

            if (Settings.Default.SyncError)
            {
                Debug.WriteLine("Sync Error");
                LocalToCentralSync();
            }
            else
            {
                Debug.WriteLine("No Sync Error");
            }
        }

        public ObservableCollection<SaveDeleteMessage> theSaveDeleteMessages { get; set; }
        public ObservableCollection<UpdateMessage> theUpdateMessages { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand LCSyncCommand { get; set; }
        public ICommand CLSyncCommand { get; set; }
        public ICommand ShowUpdateMessage { get; set; }
        public ICommand DeleteUpdateMessage { get; set; }

        public UpdateMessage SelectedUpdateMessage { get; set; }

        #region back
        private void PerformBack()
        {
            Navigator.SetNewView(new LoginView());
        }
        #endregion

        #region Central to Local Sync

        private async void CentralToLocalSync()
        {
            var _message = new SaveDeleteMessage();
            _message.Action = "CtL Synchroniseren";

            var task = Task.Run(() =>
            {
                if (theUpdateMessages.Count == 0)
                {
                    _message.Date = DateTime.Now;
                    if (Settings.Default.OnOffline)
                    {
                        //Start sync with central
                        var syncCToL = ldb.SyncCentralToLocal();

                        if (syncCToL.Result)
                            _message.Message = "Succes!";
                        else
                            _message.Message = "Failed!";
                    }
                    else
                    {
                        _message.Message =
                            "Kon niet gaan synchroniseren want er is geen verbinding met de centrale database!";
                    }
                }
            });

            await task;
            theSaveDeleteMessages.Add(_message);
        }

        #endregion

        #region Conflict Buttons

        private void ShowUpdateMessageDialog()
        {
            if (SelectedUpdateMessage != null)
            {
                var window = new UpdateConflictDialog(SelectedUpdateMessage, ldb);
                window.ShowDialog();

                if (window.DialogResult.HasValue && window.DialogResult.Value)
                    theUpdateMessages.Remove(
                        theUpdateMessages.Where(i => i.LocalDatabaseName == SelectedUpdateMessage.LocalDatabaseName)
                            .Where(i => i.LocalId == SelectedUpdateMessage.LocalId)
                            .Single());
                CentralToLocalSync();
            }
        }

        private void DeleteUpdateMessageDialog()
        {
            if (SelectedUpdateMessage != null)
            {
                var window = new DeleteDialog();
                window.ShowDialog();

                if (window.DialogResult.HasValue && window.DialogResult.Value)
                    theUpdateMessages.Remove(
                        theUpdateMessages.Where(i => i.LocalDatabaseName == SelectedUpdateMessage.LocalDatabaseName)
                            .Where(i => i.LocalId == SelectedUpdateMessage.LocalId)
                            .Single());
                CentralToLocalSync();
            }
        }

        #endregion

        #region Local to Central Sync

        public async void LocalToCentralSync()
        {
            List<SaveDeleteMessage> SaveDeleteMessage = null;
            Tuple<List<UpdateMessage>, SaveDeleteMessage> UpdateMessages = null;
            List<UpdateMessage> UpdateMessage = null;

            var task = Task.Run(() =>
            {
                SaveDeleteMessage = SaveDelete(ldb).Result;
                UpdateMessages = Update(ldb).Result;
                UpdateMessage = UpdateMessages.Item1;
            });

            await task;

            foreach (var m in SaveDeleteMessage)
                theSaveDeleteMessages.Add(m);

            theSaveDeleteMessages.Add(UpdateMessages.Item2);

            foreach (var m in UpdateMessage)
                theUpdateMessages.Add(m);

            CentralToLocalSync();
        }

        private static async Task<List<SaveDeleteMessage>> SaveDelete(LocalDatabaseMain ldb)
        {
            List<SaveDeleteMessage> message = null;

            var task = Task.Run(() => { message = ldb.SyncLocalToCentralSaveDelete(); });

            await task;

            return message;
        }

        private static async Task<Tuple<List<UpdateMessage>, SaveDeleteMessage>> Update(LocalDatabaseMain ldb)
        {
            Tuple<List<UpdateMessage>, SaveDeleteMessage> UpdateMessages = null;

            var task = Task.Run(() => { UpdateMessages = ldb.SyncLocalToCentralUpdate(); });

            await task;

            return UpdateMessages;
        }

        #endregion
    }
}