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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.View
{
	/// <summary>
	/// Interaction logic for EmployeeCreationView.xaml
	/// </summary>
	public partial class EmployeeCreationView : UserControl, IHavePassword
	{
		public EmployeeCreationView()
		{
			InitializeComponent();
		}

        public System.Security.SecureString Password
        {
            get { return MynPasswordBox.SecurePassword; }
        }
    }
}
