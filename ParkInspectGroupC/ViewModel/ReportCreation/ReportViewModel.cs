using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
		public ObservableCollection<Diagram> ReportSectionDiagrams { get; set; }
		public ObservableCollection<InspectionImage> ReportSectionImages { get; set; }

		public Assignment SelectedAssignment { get; set; }
		public Report SelectedReport { get; set; }
		public Employee SelectedEmployee { get; set; }
		public ReportSection SelectedReportSection { get; set; }

		// Commands
		public ICommand SaveReportCommand { get; set; }
		public ICommand OpenAssignmentCommand { get; set; }
		public ICommand OpenReportCommand { get; set; }
		public ICommand DeleteReportCommand { get; set; }
		public ICommand CreateReportCommand { get; set; }
		public ICommand OpenReportSectionCommand { get; set; }
		public ICommand DeleteReportSectionCommand { get; set; }
		public ICommand CreateRepportSectionCommand { get; set; }

		public string ReportSectionTitle
		{
			get { return SelectedReportSection.Title; }
			set { SelectedReportSection.Title = value; RaisePropertyChanged("ReportSectionTitle"); }
		}

		public string ReportSectionDescription
		{
			get { return SelectedReportSection.Summary; }
			set { SelectedReportSection.Summary = value; RaisePropertyChanged("ReportSectionDescription"); }
		} 

		public string ReportTitle
		{
			get { return SelectedReport.Title; }
			set { SelectedReport.Title = value; RaisePropertyChanged("ReportTitle"); }
		}
		
		public string ReportDescription
		{
			get { return SelectedReport.Summary; }
			set { SelectedReport.Summary = value; RaisePropertyChanged("ReportDescription"); }
		}

		public ReportViewModel()
		{
			SaveReportCommand = new RelayCommand(SaveReport, CanSaveReport);
			OpenAssignmentCommand = new RelayCommand(OpenAssignmentView);
			OpenReportCommand = new RelayCommand(OpenRepport);
			DeleteReportCommand = new RelayCommand(DeleteRepport);
			CreateReportCommand = new RelayCommand(CreateReport);
			OpenReportSectionCommand = new RelayCommand(OpenReportSection);
			DeleteReportSectionCommand = new RelayCommand(DeleteRepportSection);
			CreateRepportSectionCommand = new RelayCommand(CreateRepportSection);

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
					Summary = "Tijdige omschrijving",
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

			ReportTitle = SelectedReport.Title;
			ReportDescription = SelectedReport.Summary;

			Navigator.SetNewView(new ReportView());
		}

		private void SaveReport()
		{
			using (var context = new ParkInspectEntities())
			{
				// Report itself.
				var report = (from a in context.Reports where a.Id == SelectedReport.Id select a).FirstOrDefault();

				report.Title = ReportTitle;
				report.Summary = ReportDescription;

				// Report sections
				foreach (var section in ReportSectionList)
				{
					var s = (from a in context.ReportSections where a.Id == section.Id select a).FirstOrDefault();

					s.Title = section.Title;
					s.Summary = section.Summary;
					//s.Diagrams = section.Diagrams;
					//s.InspectionImages = section.InspectionImages;

					context.Entry(s).State = EntityState.Modified;
				}

				context.SaveChanges();
			}
		}

		private void OpenReportSection()
		{
			using (var context = new ParkInspectEntities())
			{
				var section = (from a in context.ReportSections where a.Id == SelectedReportSection.Id select a).FirstOrDefault();

				//ReportSectionTitle = section.Title;
				//ReportSectionDescription = section.Summary;

				//ReportSectionImages = new ObservableCollection<InspectionImage>(section.InspectionImages);
				//ReportSectionDiagrams = new ObservableCollection<Diagram>(section.Diagrams);

			}

			Navigator.SetNewView(new ReportSectionView());
		}

		private void DeleteRepport()
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("Weet je het zeker?", "Rapport Verwijder bevestiging",  MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				using (var context = new ParkInspectEntities())
				{
					var report = (from a in context.Reports where a.Id == SelectedReport.Id select a).FirstOrDefault();

					context.Reports.Remove(report);
					context.SaveChanges();

					ReportList.Remove(SelectedReport);
				}
 			}
		}

		private bool CanSaveReport()
		{
			if (string.IsNullOrWhiteSpace(ReportTitle) || string.IsNullOrWhiteSpace(ReportDescription))
				return false;

			return true;
		}

		private void CreateRepportSection()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportSection = new ReportSection()
				{
					Title = "Tijdige titel",
					Summary = "Tijdige omschrijving",
					ReportId = SelectedReport.Id
				};

				context.ReportSections.Add(reportSection);
				context.SaveChanges();
				ReportSectionList.Add(reportSection);
			}
		}

		private void DeleteRepportSection()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportSection = (from a in context.ReportSections where a.Id == SelectedReportSection.Id select a).FirstOrDefault();

				context.ReportSections.Remove(reportSection);
				context.SaveChanges();
				ReportSectionList.Remove(SelectedReportSection);
			}
		}
	}
}
