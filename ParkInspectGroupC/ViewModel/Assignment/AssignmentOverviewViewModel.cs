using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentOverviewViewModel : ViewModelBase
    {
        public AssignmentOverviewViewModel()
        {
            _searchCritetia = "";
            fillAllAssignments();

            SearchAll = new RelayCommand(refillCollection);
            EditCommand = new RelayCommand(EditAssignment);
            ShowDetails = new RelayCommand(showDetails);
            newInspection = new RelayCommand(createInspection);
            newAssigment = new RelayCommand(makeNewAssignment);
        }

        public void fillAllAssignments()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    AssignmentCollection = context.Assignment.ToList();
                }
            }
            catch
            {
                AssignmentCollection = new List<Assignment>();
                var as1 = new Assignment {Id = 30, Description = "Something went wrong"};
                AssignmentCollection.Add(as1);
            }

            refillCollection();
        }

        private void refillCollection()
        {
            IEnumerable<Assignment> tempCollection;
            if (_showClosedAssignments)
                tempCollection = from Assignment in AssignmentCollection
                    orderby Assignment.Id ascending
                    where Assignment.Description.Contains(_searchCritetia)
                    select Assignment;
            else
                tempCollection = from Assignment in AssignmentCollection
                    orderby Assignment.Id ascending
                    where Assignment.Description.Contains(_searchCritetia) && (Assignment.EndDate != null)
                    select Assignment;

            ObservedCollection = new ObservableCollection<Assignment>(tempCollection);

            base.RaisePropertyChanged("ObservedCollection");
        }

        private void makeNewAssignment()
        {
            Navigator.SetNewView(new NewAssignmentView());
        }

        private void showDetails()
        {
            var customerName = "";
            var managerName = "";
            var description = SelectedAssignment.Description;
            var startDate = SelectedAssignment.StartDate.ToString();
            var endDate = SelectedAssignment.EndDate.ToString();

            var details = "";

            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    customerName = context.Customer.Single(n => n.Id == SelectedAssignment.CustomerId).Name;
                    var manager = context.Employee.Single(m => m.Id == SelectedAssignment.ManagerId);
                    managerName = manager.FirstName + " " + manager.SurName;
                }

                details = "Klant: " + customerName + "; Manager: " + managerName + "\n" + "Beschrijving: " + description +
                          "\n" + "Start datum: " + startDate + "; Eind datum: " + endDate;
            }
            catch
            {
                details = "Something went wrong";
            }

            AssignmentDetails = details;
            base.RaisePropertyChanged("AssignmentDetails");
        }

        private void EditAssignment()
        {
            var EditView = new EditAssignmentView(SelectedAssignment, this);
            EditView.Show();
        }

        private void createInspection()
        {
            var converterView = new AssignmentToInspectionView();
            converterView.Show();
            ((AssignmentToInspectionViewModel) converterView.DataContext).setAssignment(SelectedAssignment);
        }

        #region properties

        private bool _showClosedAssignments;

        public bool ShowClosedAssignments
        {
            get { return _showClosedAssignments; }
            set
            {
                _showClosedAssignments = value;
                refillCollection();
            }
        }

        private string _searchCritetia;

        public string SearchCriteria
        {
            get { return _searchCritetia; }
            set
            {
                _searchCritetia = value;
                refillCollection();
            }
        }

        public Assignment SelectedAssignment { get; set; }


        public List<Assignment> AssignmentCollection { get; set; }

        public ObservableCollection<Assignment> ObservedCollection { get; set; }

        public string AssignmentDetails { get; set; }
        public ICommand newAssigment { get; set; }

        public ICommand SearchAll { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ShowDetails { get; set; }
        public ICommand newInspection { get; set; }


        #endregion
    }
}