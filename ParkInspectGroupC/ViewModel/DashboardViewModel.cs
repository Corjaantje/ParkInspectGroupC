using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel()
        {
            Employee = Settings.Default.LoggedInEmp;
            using (var context = new LocalParkInspectEntities())
            {
                var eAvailability = context.Availability.Where(e => e.EmployeeId == Employee.Id).ToList();
                EmployeeAvailability = new ObservableCollection<Availability>(eAvailability);
            }
        }

        public Employee Employee { get; set; }
        public ObservableCollection<Availability> EmployeeAvailability { get; set; }
    }
}