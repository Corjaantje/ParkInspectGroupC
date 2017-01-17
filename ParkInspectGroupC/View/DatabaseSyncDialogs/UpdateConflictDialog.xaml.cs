using System.Windows;
using LocalDatabase;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.View.DatabaseSyncDialogs
{
    /// <summary>
    ///     Interaction logic for UpdateConflictDialog.xaml
    /// </summary>
    public partial class UpdateConflictDialog : Window
    {
        private readonly LocalDatabaseMain ldb;
        private readonly UpdateMessage message;

        public UpdateConflictDialog(UpdateMessage message, LocalDatabaseMain ldb)
        {
            InitializeComponent();
            this.ldb = ldb;
            this.message = message;
            LoadDetails(message);
        }

        private void LoadDetails(UpdateMessage message)
        {
            var msgLocal = ldb.GetLocalDetails(message);
            var msgCentral = ldb.GetCentralDetails(message);

            tbLocal.Text = msgLocal;
            tbCentral.Text = msgCentral;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btLocal_Click(object sender, RoutedEventArgs e)
        {
            ldb.KeepLocal(message);
            DialogResult = true;
            Close();
        }

        private void btCentral_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}