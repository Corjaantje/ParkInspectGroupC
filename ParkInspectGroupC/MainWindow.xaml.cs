using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using LocalDatabase;
using LocalDatabase.Domain;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Timer aTimer;
        private readonly LocalDatabaseMain ldb;

        public MainWindow()
        {
            InitializeComponent();

            //Create local db
            ldb = new LocalDatabaseMain("ParkInspect");

            //Setup settings for automatic sync
            theSaveDeleteMessages = new ObservableCollection<SaveDeleteMessage>();
            theUpdateMessages = new ObservableCollection<UpdateMessage>();

            aTimer = new Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 3600000; //Every hour
            //aTimer.Interval = 10000;//Every 20 seconds, test setting
            aTimer.Enabled = true;
        }

        public ObservableCollection<SaveDeleteMessage> theSaveDeleteMessages { get; set; }
        public ObservableCollection<UpdateMessage> theUpdateMessages { get; set; }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("Timer ended, Starting Auto Sync");
            Debug.WriteLine("Stopping timer");
            aTimer.Stop();
            AutoDatabaseSync();
        }

        private async void AutoDatabaseSync()
        {
            if (!Settings.Default.SyncError)
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

                if (UpdateMessage.Count == 0)
                    CentralToLocalSync();
                else
                    Settings.Default.SyncError = true;
            }
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

        private async void CentralToLocalSync()
        {
            var task = Task.Run(() =>
            {
                if (Settings.Default.OnOffline)
                {
                    //Start sync with central
                    var syncCToL = ldb.SyncCentralToLocal();

                    if (syncCToL.Result)
                        Settings.Default.SyncError = false;
                    else
                        Settings.Default.SyncError = true;
                }
                aTimer.Start();
                Debug.WriteLine("Timer started");
            });

            await task;
        }
    }
}