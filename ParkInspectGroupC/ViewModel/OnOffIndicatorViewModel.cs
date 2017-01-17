using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class OnOffIndicatorViewModel : ViewModelBase
    {
        private static Timer BackgroundWorkerTimer;
        private Brush _IndicatorColor;
        private string _OnOffIndicator;
        private LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");
        private readonly BackgroundWorker worker;

        public OnOffIndicatorViewModel()
        {
            CheckConnection();

            //Setup backgroundworker
            worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            worker.DoWork += worker_DoWork;

            //Setup timer that fires the backgroundworker when timer ends
            //BackgroundWorkerTimer = new Timer(3600000);//An hour, standard
            BackgroundWorkerTimer = new Timer(20000);
                //20 seconds, Testing setting, you can set it sorter but when the timer start hits 0 it start again and does not wait for the worker to complete, thus it is possible that 2 workers are running at the same time when setting on less than 20 seconds
            BackgroundWorkerTimer.Elapsed += BackgroundWorkerTimer_Elapsed;
            BackgroundWorkerTimer.Enabled = true;

            GoTo = new RelayCommand(GoToView);
        }

        public ICommand GoTo { get; set; }

        public string OnOffIndicator
        {
            get { return _OnOffIndicator; }
            set
            {
                _OnOffIndicator = value;
                RaisePropertyChanged("OnOffIndicator");
            }
        }

        public Brush IndicatorColor
        {
            get { return _IndicatorColor; }
            set
            {
                _IndicatorColor = value;
                RaisePropertyChanged("IndicatorColor");
            }
        }

        public void GoToView()
        {
            Navigator.SetNewView(new DatabaseSyncView());
        }

        public async void CheckConnection()
        {
            var task = Task.Run(() =>
            {
                OnOffIndicator = "Checking...";
                var bc = new BrushConverter();
                IndicatorColor = (Brush) bc.ConvertFrom("Orange");
                //Check to see if there is a connection with the central db (Azure db)
                using (var db = new ParkInspectEntities())
                {
                    var conn = db.Database.Connection;
                    try
                    {
                        conn.Open();
                        Settings.Default.OnOffline = true;
                        OnOffIndicator = "Online";
                        IndicatorColor = (Brush) bc.ConvertFrom("LightGreen");
                        Debug.WriteLine("Online (Azure connection)");
                    }
                    catch
                    {
                        Settings.Default.OnOffline = false;
                        OnOffIndicator = "Offline";
                        IndicatorColor = (Brush) bc.ConvertFrom("Red");
                        Debug.WriteLine("Offline  (Azure connection)");
                    }
                }
            });

            await task;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("Checking azure connection...");
            CheckConnection();
        }

        private void BackgroundWorkerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            worker.RunWorkerAsync();
        }
    }
}