using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ParkInspectGroupC.Miscellaneous;
using LocalDatabase;
using LocalDatabase.Domain;

namespace ParkInspectGroupC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
			InitializeComponent();

            //Create local db
            LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");

            //Start sync with central
            //bool syncCToL = ldb.SyncCentralToLocal();
            //MessageBox.Show("Sync was: " + syncCToL);//true = good, false = bad

            //List<SaveDeleteMessage> syncTwo = ldb.SyncLocalToCentralSaveDelete();
            //string syncLtoC = null;
            //foreach (string m in syncTwo)
            //{
            //    syncLtoC += m + Environment.NewLine;
            //}
            //MessageBox.Show(syncLtoC);//true = good, false = bad
        }
    }
}
