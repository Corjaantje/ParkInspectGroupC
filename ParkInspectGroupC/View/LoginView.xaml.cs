using System.Security;
using System.Windows.Controls;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IHavePassword
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public SecureString Password
        {
            get { return MyPasswordBox.SecurePassword; }
        }
    }
}