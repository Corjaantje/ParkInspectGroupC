using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
	class InspectionViewModel : ViewModelBase
	{

		#region properties

		// maybe create fancier location search criteria
		private String _searchCriteria;
		public String SearchCriteria
		{
			get { return _searchCriteria; }
			set { _searchCriteria = value; }
		}

		private ObservableCollection<Inspection> _inspections;
		public ObservableCollection<Inspection> Inspections
		{
			get { return _inspections; }
			set { _inspections = value; }
		}

		public ICommand StartSearch { get; set; }

		private List<Inspection> allInspections;
		#endregion


		public InspectionViewModel()
		{
			_searchCriteria = "";
			fillInspections();

			StartSearch = new RelayCommand(refillObservableCollection);

		}

		private void fillInspections()
		{
			try
			{
				using (var context = new LocalParkInspectEntities())
				{

					var result = context.Inspection.ToList();
					allInspections = new List<Inspection>(result);

				}
			}
			catch
			{

				allInspections = new List<Inspection>();
				Inspections.Add(new Inspection { Id = 100, Location = "Something went wrong" });

			}

			refillObservableCollection();
		}

		private void refillObservableCollection()
		{

			var result = from Inspection in allInspections orderby Inspection.Id ascending where Inspection.Location.Contains(_searchCriteria) select Inspection;
			Inspections = new ObservableCollection<Inspection>(allInspections);

			base.RaisePropertyChanged("Inspections");

		}


	}
}
