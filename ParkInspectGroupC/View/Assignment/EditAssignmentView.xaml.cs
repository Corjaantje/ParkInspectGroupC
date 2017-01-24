using System.Windows;
using LocalDatabase.Domain;
using ParkInspectGroupC.ViewModel;
using System.Windows.Controls;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for EditAssignmentView.xaml
    /// </summary>
    public partial class EditAssignmentView : UserControl
    {
        public EditAssignmentView(Assignment a, AssignmentOverviewViewModel avm)
        {
            InitializeComponent();
            DataContext = new EditAssignmentViewModel(a, avm);
        }
    }
}