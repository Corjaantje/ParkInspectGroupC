using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorListViewModel : ViewModelBase
    {
        private ObservableCollection<Availability> _inspectorAvailability;

        private Availability _selectedAailability;
        private Employee _selectedInspector;

        private bool _showAvailability;
        private int days;

        public InspectorListViewModel()
        {
            ShowAvailability = false;
            using (var context = new LocalParkInspectEntities())
            {
                var iList = context.Employee.Where(e => e.IsManager == false).ToList();
                InspectorsList = new ObservableCollection<Employee>(iList);
            }
            ShowEditInspectorCommand = new RelayCommand(ShowEditView);
            ShowCreateAvailabilityCommand = new RelayCommand(ShowCreateAvailability);
            ShowEditAvailabilityCommand = new RelayCommand(ShowEditAvailability, CanShowAvailabilityEdit);
            ShowInspectorInspectionsCommand = new RelayCommand(ShowInspectorInspections);
        }

        public Employee SelectedInspector
        {
            get { return _selectedInspector; }
            set
            {
                _selectedInspector = value;
                RaisePropertyChanged("SelectedInspector");
                // show availability datagrid of the selectedinspector
                if (value != null)
                {
                    using (var context = new LocalParkInspectEntities())
                    {
                        var iAvailability =
                            context.Availability.Where(e => e.EmployeeId == SelectedInspector.Id).ToList();
                        InspectorAvailability = new ObservableCollection<Availability>(iAvailability);
                    }
                    ShowAvailability = true;
                }
            }
        }

        public Availability SelectedAvailability
        {
            get { return _selectedAailability; }
            set
            {
                _selectedAailability = value;
                RaisePropertyChanged("SelectedAvailability");
            }
        }

        public ObservableCollection<Employee> InspectorsList { get; set; }

        public ObservableCollection<Availability> InspectorAvailability
        {
            get { return _inspectorAvailability; }
            set
            {
                _inspectorAvailability = value;
                RaisePropertyChanged("InspectorAvailability");
            }
        }

        public ICommand ShowEditInspectorCommand { get; set; }
        public ICommand ShowCreateAvailabilityCommand { get; set; }
        public ICommand ShowEditAvailabilityCommand { get; set; }
        public ICommand ShowInspectorInspectionsCommand { get; set; }

        public bool ShowAvailability
        {
            get { return _showAvailability; }
            set
            {
                _showAvailability = value;
                RaisePropertyChanged("ShowAvailability");
            }
        }

        private void ShowEditView()
        {
            Settings.Default.Employee = SelectedInspector;
            Navigator.SetNewView(new InspectorEditView());
            ShowAvailability = false;
        }

        private void ShowCreateAvailability()
        {
            Settings.Default.Employee = SelectedInspector;
            Navigator.SetNewView(new AvailabilityCreationView());
            ShowAvailability = false;
        }

        private void ShowEditAvailability()
        {
            Settings.Default.Availability = SelectedAvailability;
            Navigator.SetNewView(new AvailabilityEditView());
        }

        private void ShowInspectorInspections()
        {
            Settings.Default.Employee = SelectedInspector;
            Navigator.SetNewView(new InspectorInspectionsView());
            ShowAvailability = false;
        }

        private bool CanShowAvailabilityEdit()
        {
            if (SelectedAvailability != null)
            {
                // get days between dates to see if employee may edit
                var daysBetweenDates = DateTime.Now - SelectedAvailability.Date;
                days = (int) daysBetweenDates.TotalDays;
            }

            if (days < 7)
                return false;
            return true;
        }
    }
}