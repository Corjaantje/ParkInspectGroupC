using System.Windows;
using ParkInspectGroupC.ViewModel;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for NewAssignmentView.xaml
    /// </summary>
    public partial class NewAssignmentView : Window
    {
        public NewAssignmentView()
        {
            InitializeComponent();
            DataContext = new NewAssignmentViewModel();
        }
    }
}