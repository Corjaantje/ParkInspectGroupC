using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using ParkInspectGroupC.Properties;
using System;

namespace ParkInspectGroupC.ViewModel
{
	public class AssignmentOverviewViewModel : ViewModelBase
	{
        public bool loggedInEmpIsmanager{ get; set; }
        public AssignmentOverviewViewModel()
		{
            loggedInEmpIsmanager = Properties.Settings.Default.LoggedInEmp.IsManager;
            _searchCritetia = "";
			fillAllAssignments();
			SearchAll = new RelayCommand(refillCollection);
			EditCommand = new RelayCommand(EditAssignment);
			ShowDetails = new RelayCommand(showDetails);
			ShowResultsRelay = new RelayCommand(ShowResults);
			newAssigment = new RelayCommand(makeNewAssignment);
			//ShowQuestionnaire = new RelayCommand(showQuestionnaire);
			ShowFilteredInspections = new RelayCommand(showFilteredInspections);
		}

		public void fillAllAssignments()
		{
			try
			{
				using (var context = new LocalParkInspectEntities())
				{
					AssignmentCollection = context.Assignment.Include("Customer").ToList();
				}
			}
			catch
			{
				AssignmentCollection = new List<Assignment>();
				var as1 = new Assignment { Id = 30, Description = "Something went wrong" };
				AssignmentCollection.Add(as1);
			}

			refillCollection();
		}

		public void addNewAssignment(Assignment a)
		{
			// adds a assignment to the list
			AssignmentCollection.Add(a);
			ObservedCollection.Add(a);
			RaisePropertyChanged("AssignmentCollection");
			RaisePropertyChanged("ObservedCollection");
		}

		private void refillCollection()
		{
			IEnumerable<Assignment> tempCollection;
			if (_showClosedAssignments)
				tempCollection = from Assignment in AssignmentCollection
								 orderby Assignment.Id ascending
								 where Assignment.Description.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Name.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Location.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Phonenumber.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Email.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Address.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Id.ToString().ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.DateCreated.ToString().ToLower().Contains(_searchCritetia.ToLower())
                                 select Assignment;
			else
				tempCollection = from Assignment in AssignmentCollection
								 orderby Assignment.Id ascending
								 where Assignment.Description.ToLower().Contains(_searchCritetia.ToLower()) 
                                    || Assignment.Customer.Name.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Location.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Phonenumber.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Email.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Address.ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.Customer.Id.ToString().ToLower().Contains(_searchCritetia.ToLower())
                                    || Assignment.DateCreated.ToString().ToLower().Contains(_searchCritetia.ToLower())
                                    && (Assignment.EndDate != null)
								 select Assignment;

			ObservedCollection = new ObservableCollection<Assignment>(tempCollection);

			base.RaisePropertyChanged("ObservedCollection");
		}

		private void makeNewAssignment()
		{
			Navigator.SetNewView(new NewAssignmentView());
		}

		//private void showQuestionnaire()
		//{
		//	var questionnaireView = new QuestionnaireView();
		//	questionnaireView.Show();
		//}

		private void showFilteredInspections()
		{
			if (SelectedAssignment != null)
			{
				Settings.Default.AssignmentId = SelectedAssignment.Id;
				var Inspections = new InspectionView();
				Navigator.SetNewView(Inspections);

			}
		}

		private void showDetails()
		{
			if (SelectedAssignment == null)
				return;

			var customerName = "";
			var managerName = "";
			var description = SelectedAssignment.Description;
			var startDate = ((DateTime)SelectedAssignment.StartDate).Date.ToString("d");
			var endDate = ((DateTime)SelectedAssignment.EndDate).Date.ToString("d");

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

			if (SelectedAssignment != null)
			{
				Navigator.SetNewView(new EditAssignmentView(SelectedAssignment, this));
			}
		}

		private void ShowResults()
		{
            Settings.Default.SelectedAssignmentId = SelectedAssignment.Id;
            Navigator.SetNewView(new AssignmentResultView());
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
		public ICommand ShowResultsRelay { get; set; }
		//public ICommand ShowQuestionnaire { get; set; }
		public ICommand ShowFilteredInspections { get; set; }

		#endregion
	}
}