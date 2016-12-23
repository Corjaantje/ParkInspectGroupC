using LocalDatabase.Domain;
using ParkInspectGroupC.ViewModel;
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

namespace ParkInspectGroupC.View
{
	/// <summary>
	/// Interaction logic for EditAssignmentView.xaml
	/// </summary>
	public partial class EditAssignmentView : Window
	{
		public EditAssignmentView(Assignment a, AssignmentOverviewViewModel avm)
		{
			InitializeComponent();
			DataContext = new EditAssignmentViewModel(a, avm);
		}
	}
}
