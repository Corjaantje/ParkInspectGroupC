using System.Security;
using System.Windows.Controls;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for EmployeeCreationView.xaml
    /// </summary>
    public partial class EmployeeCreationView : UserControl, IHavePassword
    {
        public EmployeeCreationView()
        {
            InitializeComponent();
        }

        public SecureString Password
        {
            get { return MynPasswordBox.SecurePassword; }
        }
    }
}