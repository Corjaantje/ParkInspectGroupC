using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ParkInspectGroupC.DOMAIN;

namespace ParkInspectGroupC.ViewModel.ReportCreation
{
	public class ReportViewModel : ViewModelBase
	{

		public ObservableCollection<Assignment> AssignmentList { get; set; }
		public ObservableCollection<Report> ReportList { get; set; }
		public ObservableCollection<ReportSection> ReportSectionList { get; set; }

		public Assignment SelectedAssignment { get; set; }

		public Employee SelectedEmployee { get; set; }

		// Commands
		public ICommand SaveReport { get; set; }

		private string _reportTitle;
		public string ReportTitle
		{
			get { return _reportTitle; }
			set { _reportTitle = value; RaisePropertyChanged("ReportTitle"); }
		}

		private string _reportDescription;

		public string ReportDescription
		{
			get { return _reportDescription; }
			set { _reportDescription = value; RaisePropertyChanged("ReportDescription"); }
		}

		public ReportViewModel()
		{
			SaveReport = new RelayCommand(SaveReportCommand, CanSaveReport);

			using (var context = new ParkInspectEntities())
			{
				//TODO is voor test; weghalen!
				SelectedEmployee = (from a in context.Employee where a.FirstName == "Jaqueline" select a).FirstOrDefault();


				var assignmentList = (from a in context.Assignment where a.ManagerId == SelectedEmployee.Id select a).ToList();
				AssignmentList = new ObservableCollection<Assignment>(assignmentList);
			}
		}

		private void SaveReportCommand()
		{
			
		}

		private bool CanSaveReport()
		{
			if (string.IsNullOrWhiteSpace(ReportTitle) || string.IsNullOrWhiteSpace(ReportDescription))
				return false;

			return true;
		}
	}
}
