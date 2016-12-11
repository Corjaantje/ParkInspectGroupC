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
        }
    }
}
