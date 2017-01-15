using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
	public class AssignmentOverviewViewModel : ViewModelBase
	{

		#region properties
		private Boolean _showClosedAssignments;
		public Boolean ShowClosedAssignments
		{
			get { return _showClosedAssignments; }
			set { _showClosedAssignments = value; this.refillCollection(); }
		}

		private String _searchCritetia;
		public String SearchCriteria
		{
			get { return _searchCritetia; }
			set { _searchCritetia = value; refillCollection();}
		}

		private Assignment _selectedAssignment;
		public Assignment SelectedAssignment
		{
			get { return _selectedAssignment; }
			set { _selectedAssignment = value; }
		}


		public List<Assignment> AssignmentCollection { get; set; }

		private ObservableCollection<Assignment> _observedCollection;
		public ObservableCollection<Assignment> ObservedCollection
		{
			get { return _observedCollection; }
			set { _observedCollection = value; }
		}

		private String _assignmentDetails;
		public String AssignmentDetails {
			get { return _assignmentDetails; }
			set { _assignmentDetails = value; }
		}

		public ICommand SearchAll { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand ShowDetails { get; set; }
		public ICommand newInspection { get; set; }
		#endregion

		public AssignmentOverviewViewModel()
		{
			_searchCritetia = "";
			fillAllAssignments();

			SearchAll = new RelayCommand(refillCollection);
			EditCommand = new RelayCommand(EditAssignment);
			ShowDetails = new RelayCommand(showDetails);
			newInspection = new RelayCommand(createInspection);

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
				Assignment as1 = new Assignment { Id = 30, Description = "Something went wrong" };
				AssignmentCollection.Add(as1);

			}

			refillCollection();


		}

		private void refillCollection()
		{
			IEnumerable<Assignment> tempCollection;
			if (_showClosedAssignments)
			{
				tempCollection = from Assignment in AssignmentCollection orderby Assignment.Id ascending where Assignment.Description.Contains(_searchCritetia) select Assignment;
			}
			else
			{
				tempCollection = from Assignment in AssignmentCollection orderby Assignment.Id ascending where Assignment.Description.Contains(_searchCritetia) && Assignment.EndDate != null select Assignment;
			}

			ObservedCollection = new ObservableCollection<Assignment>(tempCollection);

			base.RaisePropertyChanged("ObservedCollection");

		}

		private void showDetails()
		{

			String customerName = "";
			String managerName = "";
			String description = _selectedAssignment.Description;
			String startDate = _selectedAssignment.StartDate.ToString();
			String endDate = _selectedAssignment.EndDate.ToString();

			String details = "";

			try
			{

				using (var context = new LocalParkInspectEntities())
				{

					customerName = context.Customer.Single(n => n.Id == _selectedAssignment.CustomerId).Name;
					Employee manager = context.Employee.Single(m => m.Id == _selectedAssignment.ManagerId);
					managerName = manager.FirstName + " " + manager.SurName;

				}

				details = "Klant: " + customerName + "; Manager: " + managerName + "\n" + "Beschrijving: " + description + "\n" + "Start datum: " + startDate + "; Eind datum: " + endDate;
				

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

			EditAssignmentView EditView = new EditAssignmentView(SelectedAssignment, this);
			EditView.Show();

		}

		private void createInspection()
		{

			AssignmentToInspectionView converterView = new AssignmentToInspectionView();
			converterView.Show();
			((AssignmentToInspectionViewModel)converterView.DataContext).setAssignment(_selectedAssignment);

		}

	}

}
