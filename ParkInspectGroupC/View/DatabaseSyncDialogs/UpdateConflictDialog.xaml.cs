using LocalDatabase;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParkInspectGroupC.View.DatabaseSyncDialogs
{
    /// <summary>
    /// Interaction logic for UpdateConflictDialog.xaml
    /// </summary>
    public partial class UpdateConflictDialog : Window
    {
        LocalDatabaseMain ldb;
        UpdateMessage message;
        public UpdateConflictDialog(UpdateMessage message, LocalDatabaseMain ldb)
        {
            InitializeComponent();
            this.ldb = ldb;
            this.message = message;
            LoadDetails(message);
        }
        private void LoadDetails(UpdateMessage message)
        {
            string msgLocal = ldb.GetLocalDetails(message);
            string msgCentral = ldb.GetCentralDetails(message);

            tbLocal.Text = msgLocal;
            tbCentral.Text = msgCentral;
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btLocal_Click(object sender, RoutedEventArgs e)
        {
            ldb.KeepLocal(message);
            DialogResult = true;
            this.Close();
        }
        private void btCentral_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
