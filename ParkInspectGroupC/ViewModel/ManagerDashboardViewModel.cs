using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class ManagerDashboardViewModel : ViewModelBase
    {
        public ManagerDashboardViewModel()
        {
            Manager = Settings.Default.LoggedInEmp;
            using (var context = new LocalParkInspectEntities())
            {
                var asList = (from a in context.Assignment
                    join c in context.Customer on a.CustomerId equals c.Id
                    where a.ManagerId == Manager.Id
                    select new {c.Name, a.Description}).ToList();
                AssignmentList = new ObservableCollection<object>(asList);

                var mAvailability = context.Availability.Where(e => e.EmployeeId == Manager.Id).ToList();
                ManagerAvailability = new ObservableCollection<Availability>(mAvailability);
            }
            ShowInspectorListCommand = new RelayCommand(ShowInspectorList);
            AddAccountCommand = new RelayCommand(ShowAddAccount);
        }

        public Employee Manager { get; set; }

        public ObservableCollection<object> AssignmentList { get; set; }
        public ObservableCollection<Availability> ManagerAvailability { get; set; }
        public ICommand ShowInspectorListCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }

        private void ShowInspectorList()
        {
            Navigator.SetNewView(new InspectorsListView());
        }

        private void ShowAddAccount()
        {
            Navigator.SetNewView(new EmployeeCreationView());
        }
    }
}