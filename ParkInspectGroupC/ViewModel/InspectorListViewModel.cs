using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
   public class InspectorListViewModel: ViewModelBase
    {
        private Employee _selectedInspector;
        public Employee SelectedInspector
        {
            get
            {
                return _selectedInspector;
            }
            set
            {
                _selectedInspector = value; RaisePropertyChanged("SelectedInspector");
                if (value != null)
                {
                    using (var context = new LocalParkInspectEntities())
                    {
                        var iAvailability = context.Availability.Where(e => e.EmployeeId == SelectedInspector.Id).ToList();
                        InspectorAvailability = new ObservableCollection<Availability>(iAvailability);
                    }
                    ShowAvailability = true;
                }
            }
        }

        public ICommand ShowEditInspectorCommand { get; set; }
        public ICommand ShowEditAvailabilityCommand { get; set; }
        public ObservableCollection<Employee> InspectorsList { get; set; }

        private ObservableCollection<Availability> _inspectorAvailability;
        public ObservableCollection<Availability> InspectorAvailability
        {
            get { return _inspectorAvailability; }
            set { _inspectorAvailability = value; RaisePropertyChanged("InspectorAvailability"); }
        }
        public InspectorListViewModel()
        {
            ShowAvailability = false;
            using (var context = new LocalParkInspectEntities())
            {
                var iList = context.Employee.Where(e => e.IsManager == false).ToList();
                InspectorsList = new ObservableCollection<Employee>(iList);
            }
            ShowEditInspectorCommand = new RelayCommand(ShowEditView, CanShowEditView);
            ShowEditAvailabilityCommand = new RelayCommand(ShowEditAvailability, CanShowEditAvailibility);
        }

        private void ShowEditView()
        {
            Navigator.SetNewView(new InspectorEditView());
        }

        private bool CanShowEditView()
        {
            if (SelectedInspector == null)
            {
                return false;
            }
            return true;
        }

        private void ShowEditAvailability()
        {
            Navigator.SetNewView(new AvailabilityCreationView());
        }

        private bool CanShowEditAvailibility()
        {
            if (SelectedInspector == null)
            {
                return false;
            }
            return true;
        }
        private bool _showAvailability;
        public bool ShowAvailability
        {
            get { return _showAvailability; }
            set { _showAvailability = value; RaisePropertyChanged("ShowAvailability"); }
        }
    }
}
