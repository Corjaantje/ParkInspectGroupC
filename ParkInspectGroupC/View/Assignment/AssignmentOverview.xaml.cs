using System.Windows.Controls;
using ParkInspectGroupC.ViewModel;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for AssignmentOverview.xaml
    /// </summary>
    public partial class AssignmentOverview : UserControl
    {
        public AssignmentOverview()
        {
            InitializeComponent();

            DataContext = new AssignmentOverviewViewModel();
        }
    }
}