using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
	internal class NewAssignmentViewModel : ViewModelBase
	{
		#region properties

		private String _description;

		public String Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private String _topLabel;

		public String TopLabel
		{
			get { return _topLabel; }
			set { _topLabel = value; }
		}

		private String _createdAssignment;

		public String CreatedAssignment
		{
			get { return _createdAssignment; }
			set { _createdAssignment = value; }
		}

		private ObservableCollection<String> _allCustomerNames;

		public ObservableCollection<String> AllCustomerNames
		{
			get { return _allCustomerNames; }
			set { _allCustomerNames = value; }
		}

		private int _customerIndex;
		public int CustomerIndex
		{
			get { return _customerIndex; }
			set { _customerIndex = value; selectedCustomerChanged(); }
		}

		private String _customerDescription;
		public String CustomerDescription
		{
			get { return _customerDescription; }
			set { _customerDescription = value; }
		}

		public ICommand CreateAssignment { get; set; }

		#endregion properties

		private List<Customer> allCustomers;

		public NewAssignmentViewModel()
		{
			TopLabel = " Nieuwe opdracht";
			CreatedAssignment = "";

			generateAllCustomers();

			CreateAssignment = new RelayCommand(createAssignment);
		}

		public void createAssignment()
		{
			try
			{
				using (var context = new LocalParkInspectEntities())
				{
					var assign = new Assignment();

					assign.Id = context.Assignment.Max(u => u.Id) + 1;
					assign.CustomerId = getCustomerId();
					assign.ManagerId = getManager();
					assign.Description = _description;
					assign.DateCreated = DateTime.Today;
					assign.DateUpdated = DateTime.Today;

					context.Assignment.Add(assign);
					context.SaveChanges();
				}

				TopLabel = "Opdracht aangemaakt";

				CreatedAssignment = Description;

				Description = "";

				RaisePropertyChanged();
			}
			catch
			{
				TopLabel = "Something went wrong, changes are not saved";

				RaisePropertyChanged();
			}
		}

		private void selectedCustomerChanged()
		{
			try
			{
				using (var context = new LocalParkInspectEntities())
				{

					Customer customer = context.Customer.Single(c => c.Id == _customerIndex + 1);

					CustomerDescription = customer.Name + "\n" + customer.Location + "\n" + customer.Phonenumber;

					RaisePropertyChanged("CustomerDescription");

				}
			}
			catch
			{
				CustomerDescription = "Something went wrong";
				RaisePropertyChanged("CustomerDescription");
			}

		}
		 
		private void generateAllCustomers()
		{
			
			try
			{
				using (var context = new LocalParkInspectEntities())
				{
					allCustomers = context.Customer.ToList();

					var tempArray = new List<String>();

					foreach (Customer c in allCustomers)
					{
						tempArray.Add(c.Name);
					}

					AllCustomerNames = new ObservableCollection<String>(tempArray);
				}
			}
			catch
			{
				TopLabel = "Database not working";
			}
		}


		private int getCustomerId()
		{
			try
			{
				return (int) allCustomers[_customerIndex].Id;
			}
			catch(Exception e)
			{
				Debug.Write(e.StackTrace);

				//will crash in the createAssignment method
				//error will be shown there
				return -1;
			}
		}

		private int getManager()
		{
			// needs to properly assign to a manager
			return 4;
		}
	}
}