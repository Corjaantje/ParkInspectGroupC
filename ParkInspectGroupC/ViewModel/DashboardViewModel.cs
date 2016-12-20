using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
   public class DashboardViewModel: ViewModelBase
    {
        public Employee Employee { get; set; }
        public ObservableCollection<Availability> EmployeeAvailability { get; set; }
        public DashboardViewModel(Employee employee)
        {
            Employee = employee;
            using (var context = new LocalParkInspectEntities())
            {
                var eAvailability = context.Availability.Where(e => e.EmployeeId == Employee.Id).ToList();
                EmployeeAvailability = new ObservableCollection<Availability>(eAvailability);
            }
        }
    }
}
