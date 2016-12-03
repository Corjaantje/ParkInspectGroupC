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
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View.ReportCreation;

namespace ParkInspectGroupC.ViewModel.ReportCreation
{
	public class ReportViewModel : ViewModelBase
	{

		public ObservableCollection<Assignment> AssignmentList { get; set; }
		public ObservableCollection<Report> ReportList { get; set; }
		public ObservableCollection<ReportSection> ReportSectionList { get; set; }

		public Assignment SelectedAssignment { get; set; }
		public Report SelectedReport { get; set; }
		public Employee SelectedEmployee { get; set; }

		// Commands
		public ICommand SaveReportCommand { get; set; }
		public ICommand OpenAssignmentCommand { get; set; }
		public ICommand OpenReportCommand { get; set; }
		public ICommand DeleteReportCommand { get; set; }
		public ICommand CreateReportCommand { get; set; }

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
			SaveReportCommand = new RelayCommand(SaveReport, CanSaveReport);
			OpenAssignmentCommand = new RelayCommand(OpenAssignmentView);
			OpenReportCommand = new RelayCommand(OpenRepport);
			CreateReportCommand = new RelayCommand(CreateReport);

			using (var context = new ParkInspectEntities())
			{
				//TODO is voor test; weghalen!
				SelectedEmployee = (from a in context.Employee where a.FirstName == "Jaqueline" select a).FirstOrDefault();


				var assignmentList = (from a in context.Assignment.Include("Customer") where a.ManagerId == SelectedEmployee.Id select a).ToList();
				AssignmentList = new ObservableCollection<Assignment>(assignmentList);
			}
		}

		private void OpenAssignmentView()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportList = (from a in context.Reports where a.AssignmentId == SelectedAssignment.Id select a).ToList();
				ReportList = new ObservableCollection<Report>(reportList);
			}

			Navigator.SetNewView(new ReportCreationView());
		}

		private void CreateReport()
		{
			using (var context = new ParkInspectEntities())
			{

				Report report = new Report()
				{
					Title = "Tijdige titel",
					Summary = "Tijdige samenvatting",
					Date = DateTime.Today,
					AssignmentId = this.SelectedAssignment.Id,
					EmployeeId = SelectedEmployee.Id
				};

				context.Reports.Add(report);
				context.SaveChanges();

				ReportList.Add(report);
			}
		}

		private void OpenRepport()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportSectionList = (from a in context.ReportSections where a.ReportId == SelectedReport.Id select a ).ToList();
				ReportSectionList = new ObservableCollection<ReportSection>(reportSectionList);
			}

			Navigator.SetNewView(new ReportView());
		}

		private void SaveReport()
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
