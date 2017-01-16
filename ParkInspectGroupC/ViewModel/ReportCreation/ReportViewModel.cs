using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
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

		// Holds the quetions that could turn into a diagram.
		public ObservableCollection<Question> ReportSectionDiagramsQuestion { get; set; }
		// Holds the inspection images that could be exportered in the report.
		public ObservableCollection<InspectionImage> ReportSectionInspectionImage { get; set; }

		public Assignment SelectedAssignment { get; set; }
		public Report SelectedReport { get; set; }
		public Employee SelectedEmployee { get; set; }
		public ReportSection SelectedReportSection { get; set; }
		public Diagram SelectedReportSectionDiagram { get; set; }
		public InspectionImage SelectedAddReportSectionInspectionImage { get; set; }
		public Question SelectedAddReportSectionDiagram { get; set; }
		public InspectionImage SelectedReportSectionImage { get; set; }

		private DiagramCreationView _dView;
		private InspectorImageCreationView _mView;

		// Commands
		public ICommand SaveReportCommand { get; set; }
		public ICommand OpenAssignmentCommand { get; set; }
		public ICommand OpenReportCommand { get; set; }
		public ICommand DeleteReportCommand { get; set; }
		public ICommand CreateReportCommand { get; set; }
		public ICommand OpenReportSectionCommand { get; set; }
		public ICommand DeleteReportSectionCommand { get; set; }
		public ICommand CreateReportSectionCommand { get; set; }
		public ICommand OpenReportSectionImagesWindowCommand { get; set; }
		public ICommand OpenReportSectionDiagramWindowCommand { get; set; }
		public ICommand ConfirmAddDiagramCommand { get; set; }
		public ICommand CancelAddDiagramCommand { get; set; }
		public ICommand DeleteReportSectionDiagramCommand { get; set; }
		public ICommand ConfirmAddImageCommand { get; set; }
		public ICommand CancelAddImageCommand { get; set; }
		public ICommand DeleteReportSectionImageCommand { get; set; }
		public ICommand GenerateReportCommand { get; set; }
		public ICommand OpenDiagramPreviewCommand { get; set; }

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
			DeleteReportCommand = new RelayCommand(DeleteRepport);
			CreateReportCommand = new RelayCommand(CreateReport);
			OpenReportSectionCommand = new RelayCommand(OpenReportSection);
			DeleteReportSectionCommand = new RelayCommand(DeleteRepportSection);
			CreateReportSectionCommand = new RelayCommand(CreateRepportSection);
			OpenReportSectionDiagramWindowCommand = new RelayCommand(OpenReportSectionDiagram);
			OpenReportSectionImagesWindowCommand = new RelayCommand(OpenReportSectionImage);
			ConfirmAddDiagramCommand = new RelayCommand(ConfirmAddReportSectionDiagram, CanConfirmAddReportSectionDiagram);
			ConfirmAddImageCommand = new RelayCommand(ConfirmAddReportSectionImage, CanConfirmAddReportSectionImage);
			DeleteReportSectionDiagramCommand = new RelayCommand(DeleteReportSectionDiagram);
			CancelAddImageCommand = new RelayCommand(CancelAddReportSectionImage);
			CancelAddDiagramCommand = new RelayCommand(CancelAddReportSectionDiagram);
			DeleteReportSectionImageCommand = new RelayCommand(DeleteReportSectionImage);
			GenerateReportCommand = new RelayCommand(GenerateReport);
			OpenDiagramPreviewCommand = new RelayCommand(OpenDiagramPreview);

			using (var context = new ParkInspectEntities())
			{
				//TODO is voor test; weghalen!
				//SelectedEmployee = (from a in context.Employee where a.FirstName == "Jaqueline" select a).FirstOrDefault();
			    SelectedEmployee = (from a in context.Employee where a.Id == Properties.Settings.Default.LoggedInEmp.Id select a).First();

				var assignmentList = (from a in context.Assignment.Include("Customer") where a.ManagerId == SelectedEmployee.Id select a).ToList();
				AssignmentList = new ObservableCollection<Assignment>(assignmentList);
			}
		}

		private void OpenDiagramPreview()
		{
			DiagramPreview dpView = new DiagramPreview();
			dpView.Show();
		}

		private void OpenAssignmentView()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportList = (from a in context.Report where a.AssignmentId == SelectedAssignment.Id select a).ToList();
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

				context.Report.Add(report);
				context.SaveChanges();

				ReportList.Add(report);
			}
		}

		private void OpenRepport()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportSectionList = (from a in context.ReportSection where a.ReportId == SelectedReport.Id select a ).ToList();
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
				var report = (from a in context.Report where a.Id == SelectedReport.Id select a).FirstOrDefault();

				report.Title = ReportTitle;
				report.Summary = ReportDescription;

				// Report sections
				foreach (var section in ReportSectionList)
				{
					var s = (from a in context.ReportSection where a.Id == section.Id select a).FirstOrDefault();

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
				var section = (from a in context.ReportSection where a.Id == SelectedReportSection.Id select a).FirstOrDefault();

				//var reportSectionDiagrams = (from a in context.Diagrams where a.Id == SelectedReport.Id select a).ToList();
				//ReportSectionDiagrams = new ObservableCollection<Diagram>(reportSectionDiagrams);
				//var reportSectionImages = (from a in )

			    var Diagrams =
			        (from a in context.Diagram.Include("Question") where a.ReportSectionId == section.Id select a).ToList();

				ReportSectionImages = new ObservableCollection<InspectionImage>(section.InspectionImage);
				ReportSectionDiagrams = new ObservableCollection<Diagram>(Diagrams);

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
					var report = (from a in context.Report where a.Id == SelectedReport.Id select a).FirstOrDefault();

                    foreach (var section in report.ReportSection)
                    {
                        context.Diagram.RemoveRange(section.Diagram);
                    }

                    context.ReportSection.RemoveRange(report.ReportSection);

					context.Report.Remove(report);
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

				context.ReportSection.Add(reportSection);
				context.SaveChanges();
				ReportSectionList.Add(reportSection);
			}
		}

		private void DeleteRepportSection()
		{
			using (var context = new ParkInspectEntities())
			{
				var reportSection = (from a in context.ReportSection where a.Id == SelectedReportSection.Id select a).FirstOrDefault();

				context.ReportSection.Remove(reportSection);
				context.SaveChanges();
				ReportSectionList.Remove(SelectedReportSection);
			}
		}

		private void OpenReportSectionDiagram()
		{
			using (var context = new ParkInspectEntities())
			{
				List<Question> questions = new List<Question>();
				List<Inspection> inspections = (from a in context.Inspection where a.AssignmentId == SelectedAssignment.Id select a).ToList();

				foreach (var inspection in inspections)
				{
					var questionnaires = inspection.Questionnaire;
					foreach (var questionaire in questionnaires)
					{
						var questionnaireModules = questionaire.QuestionnaireModule;
						foreach (var questionnaireModule in questionnaireModules)
						{
							var tempQuestions = questionnaireModule.Module.Question;
							foreach (var question in tempQuestions)
							{
								questions.Add(question);
							}
						}
					}
				}

				ReportSectionDiagramsQuestion = new ObservableCollection<Question>(questions);
			}

			_mView = new InspectorImageCreationView();
			_mView.Show();
		}

		private bool CanOpenReportSectionDiagram()
		{
			if (_dView != null && (_dView.IsEnabled || _dView.IsActive))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private bool CanOpenReportSectionImage()
		{
			if (_mView != null && (_mView.IsActive || _mView.IsEnabled))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private void CancelAddReportSectionDiagram()
		{
			_mView.Close();
		}

		private void ConfirmAddReportSectionDiagram()
		{
			using (var context = new ParkInspectEntities())
			{
				var diagram = new Diagram()
				{
					QuestionId = SelectedAddReportSectionDiagram.Id,
					ReportSectionId = SelectedReportSection.Id
				};

				context.Diagram.Add(diagram);
				context.SaveChanges();
				ReportSectionDiagrams.Add(diagram);
			}

			_mView.Close();
		}

		private void OpenReportSectionImage()
		{
			using (var context = new ParkInspectEntities())
			{
				List<InspectionImage> images = new List<InspectionImage>();

				var inspections = (from a in context.Assignment where a.Id == SelectedAssignment.Id select a).ToList();

				foreach (var inspection in inspections)
				{
					var inspectionImages =
						(from a in context.InspectionImage where a.InspectionId == inspection.Id select a).ToList();
					foreach (var inspectionImage in inspectionImages)
					{
						images.Add(inspectionImage);
					}
				}

				ReportSectionInspectionImage = new ObservableCollection<InspectionImage>(images);
			}

			_dView = new DiagramCreationView();
			_dView.Show();
		}

		private bool CanConfirmAddReportSectionDiagram()
		{
			if (SelectedAddReportSectionDiagram != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void ConfirmAddReportSectionImage()
		{
			using (var context = new ParkInspectEntities())
			{
				ReportSection section = (from a in context.ReportSection where a.Id == SelectedReportSection.Id select a).FirstOrDefault();
				InspectionImage image =
					(from a in context.InspectionImage where a.Id == SelectedAddReportSectionInspectionImage.Id select a)
						.FirstOrDefault();
				
				section.InspectionImage.Add(image);

				context.SaveChanges();

				ReportSectionImages.Add(SelectedAddReportSectionInspectionImage);
			}

			_dView.Close();
		}

		private bool CanConfirmAddReportSectionImage()
		{
			if (SelectedAddReportSectionInspectionImage != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void CancelAddReportSectionImage()
		{
			_dView.Close();
		}

		private void DeleteReportSectionImage()
		{
			using (var context = new ParkInspectEntities())
			{
				var section = (from a in context.ReportSection where a.Id == SelectedReportSection.Id select a).FirstOrDefault();
				var image =
					(from a in context.InspectionImage where a.Id == SelectedReportSectionImage.Id select a).FirstOrDefault();

				section.InspectionImage.Remove(image);
				context.SaveChanges();

				ReportSectionImages.Remove(SelectedReportSectionImage);
			}
		}

		private void DeleteReportSectionDiagram()
		{
			using (var context = new ParkInspectEntities())
			{
				var diagram = (from a in context.Diagram where a.Id == SelectedReportSectionDiagram.Id select a).FirstOrDefault();

				context.Diagram.Remove(diagram);
				context.SaveChanges();
			}

			ReportSectionDiagrams.Remove(SelectedReportSectionDiagram);
		}

		private void GenerateReport()
		{
            SaveReport();

			var fileDialog = new SaveFileDialog();
			fileDialog.DefaultExt = ".pdf";
			fileDialog.Filter = "PNG Bestanden (*.pdf)|*.pdf";

			Nullable<bool> result = fileDialog.ShowDialog();

			if (result == true)
			{
				string fileName = fileDialog.FileName.ToString();
				string filePath = fileDialog.FileName.ToString();

				PdfGenerator generator = new PdfGenerator();
				generator.GeneratePdf(SelectedReport, SelectedEmployee, fileName, filePath);
			}

		}
	}
}
