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
            Employee = Settings.Default.LoggedInEmp;
            using (var context = new LocalParkInspectEntities())
            {
                var asList = (from a in context.Assignment
                    join c in context.Customer on a.CustomerId equals c.Id
                    where a.ManagerId == Employee.Id
                    select new {c.Name, a.Description}).ToList();
                AssignmentList = new ObservableCollection<object>(asList);

                var eAvailability = context.Availability.Where(e => e.EmployeeId == Employee.Id).ToList();
                EmployeeAvailability = new ObservableCollection<Availability>(eAvailability);

                var eWorkingHours = context.WorkingHours.Where(e => e.EmployeeId == Employee.Id).ToList();
                EmployeeWorkingHours = new ObservableCollection<WorkingHours>(eWorkingHours);
            }
            ShowInspectorListCommand = new RelayCommand(ShowInspectorList);
            AddAccountCommand = new RelayCommand(ShowAddAccount);
        }

        public Employee Employee { get; set; }
        public ObservableCollection<object> AssignmentList { get; set; }
        public ObservableCollection<Availability> EmployeeAvailability { get; set; }
        public ObservableCollection<WorkingHours> EmployeeWorkingHours { get; set; }
        public ICommand ShowInspectorListCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }

        #region Buttons
        private void ShowInspectorList()
        {
            Navigator.SetNewView(new InspectorsListView());
        }
        private void ShowAddAccount()
        {
            Navigator.SetNewView(new EmployeeCreationView());
        }
        #endregion
    }
}