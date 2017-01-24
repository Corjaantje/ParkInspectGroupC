using System.Windows;

namespace ParkInspectGroupC.View.DatabaseSyncDialogs
{
    /// <summary>
    ///     Interaction logic for DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : Window
    {
        public DeleteDialog()
        {
            InitializeComponent();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}