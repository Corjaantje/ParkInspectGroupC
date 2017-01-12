using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
	class AssignmentToInspectionViewModel : ViewModelBase
	{

		#region properties

		private List<string> _regionNames;
		public List<string> RegionNames
		{
			get
			{
				return _regionNames;
			}

			set
			{
				_regionNames = value;
			}
		}

		private string _selectedRegion;
		public string SelectedRegion
		{
			get
			{
				return _selectedRegion;
			}

			set
			{
				_selectedRegion = value;
				RaisePropertyChanged("SelectedRegion");
			}
		}

		private string _location;
		public string Location
		{
			get
			{
				return _location;
			}

			set
			{
				_location = value;
				RaisePropertyChanged("Location");
			}
		}

		private DateTime _endDate;
		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}

			set
			{
				_endDate = value;
				RaisePropertyChanged("EndDate");
			}
		}

		private string _customerName;
		public string CustomerName
		{
			get
			{
				return _customerName;
			}

			set
			{
				_customerName = value;
				RaisePropertyChanged("CustomerName");
			}
		}

		private bool _AssignInspector;
		public bool AssignInspector
		{
			get
			{
				return _AssignInspector;
			}

			set
			{
				_AssignInspector = value;
			}
		}

		private List<string> _inspectorNames;
		public List<string> InspectorNames
		{
			get
			{
				return _inspectorNames;
			}

			set
			{
				_inspectorNames = value;
			}
		}

		private string _selectedInspector;
		public string SelectedInspector
		{
			get
			{
				return _selectedInspector;
			}

			set
			{
				_selectedInspector = value;
			}
		}


		public ICommand CreateInspection { get; set; }


		#endregion

		private Assignment assignment;


		public AssignmentToInspectionViewModel()
		{
			CreateInspection = new RelayCommand(createInspection);

			getAllRegions();
		}

		public void setAssignment(Assignment a)
		{
			assignment = a;
			fillProperties();
		}

		private void fillProperties()
		{
			try
			{

				using (var context = new LocalParkInspectEntities())
				{
					Customer c = context.Customer.Single(o => o.Id == assignment.CustomerId);
					CustomerName = c.Name;

					Location = c.Location;
					
					
				}

				EndDate = DateTime.Today;
				SelectedRegion = RegionNames[0];
			}
			catch(Exception e)
			{
				Debug.Write(e.StackTrace);
			}
			

		}

		private void getAllRegions()
		{
			try
			{
				using(var context = new LocalParkInspectEntities())
				{

					var regions = context.Region.ToList();

					_regionNames = new List<string>(
						from region in regions orderby region.Region1 select region.Region1
						);

				}
			}
			catch(Exception e)
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

					Inspection i = new Inspection();

					i.Id = context.Inspection.Max(o => o.Id) + 1;
					i.AssignmentId = assignment.Id;
					i.Region = context.Region.Single(o => o.Region1 == SelectedRegion);
					i.StartDate = DateTime.Today;
					i.EndDate = EndDate;
					i.StatusId = 1;

					if (_AssignInspector)
					{
						i.InspectorId = context.Employee.Single(e => e.FirstName == _selectedInspector).Id;
					}

					i.DateCreated = DateTime.Today;
					i.DateUpdated = DateTime.Today;

				}
			}
			catch(Exception e)
			{
				Debug.Write(e.StackTrace);
			}

		}

	}
}
