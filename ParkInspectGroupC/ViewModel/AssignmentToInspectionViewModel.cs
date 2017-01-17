using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentToInspectionViewModel : ViewModelBase
    {
        private Assignment assignment;


        public AssignmentToInspectionViewModel()
        {
            CreateInspection = new RelayCommand(createInspection);

            getAllRegions();
            this.getInspectorNames();
        }

        public void setAssignment(Assignment a)
        {
            assignment = a;
            fillProperties();
        }

        //Vult de lijst met inspecteur namen
        private void getInspectorNames()
        {
            InspectorNames = new List<string>();
            using (var context = new LocalParkInspectEntities())
            {
                foreach (Employee emp in context.Employee.ToList())
                {
                    if (emp.IsInspecter)
                    {
                        this.InspectorNames.Add(emp.Id + "." + emp.FirstName + " " + emp.SurName);
                    }
                }
            }
        }

        private void fillProperties()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var c = context.Customer.Single(o => o.Id == assignment.CustomerId);
                    CustomerName = c.Name;

                    Location = c.Location;
                }

                EndDate = DateTime.Today;
                SelectedRegion = RegionNames[0];
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        private void getAllRegions()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var regions = context.Region.ToList();

                    RegionNames = new List<string>(
                        from region in regions orderby region.Region1 select region.Region1
                    );
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        private void createInspection()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var i = new Inspection();

                    i.Id = context.Inspection.Max(o => o.Id) + 1;
                    i.AssignmentId = assignment.Id;
                    i.Region = context.Region.Single(o => o.Region1 == SelectedRegion);
                    i.StartDate = DateTime.Today;
                    i.EndDate = EndDate;
                    i.StatusId = 1;

                    if (AssignInspector)
                        i.InspectorId = context.Employee.Single(e => e.FirstName == SelectedInspector).Id;

                    i.DateCreated = DateTime.Today;
                    i.DateUpdated = DateTime.Today;
                }
                ErrorMessage = "De inspectie is opgeslagen";
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        #region properties

        public List<string> RegionNames { get; set; }

        private string _selectedRegion;

        public string SelectedRegion
        {
            get { return _selectedRegion; }

            set
            {
                _selectedRegion = value;
                RaisePropertyChanged("SelectedRegion");
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }

            set
            {
                _location = value;
                RaisePropertyChanged("Location");
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }

            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        private string _customerName;

        public string CustomerName
        {
            get { return _customerName; }

            set
            {
                _customerName = value;
                RaisePropertyChanged("CustomerName");
            }
        }

        public bool AssignInspector { get; set; }

        public List<string> InspectorNames { get; set; }

        public string SelectedInspector { get; set; }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }

            set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public ICommand CreateInspection { get; set; }

        //NOT YET FUNCTIONAL, ONLY ADDED DAY BEFORE DEADLINE! TODO ADD FUNCTION
        public ICommand SaveInspection { get; set; }

        #endregion
    }
}