using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ParkInspectGroupC.View.QuestionnaireModules;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.ViewModel.Questionnaire;
using GalaSoft.MvvmLight;
using System.Data.Entity.Validation;
using System.Diagnostics;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.ViewModel
{

	public class QuestionnaireViewModel : ViewModelBase
	{
        long _currentInspectionId;

		public QuestionnaireViewModel()
		{
            _currentInspectionId = Settings.Default.QuestionnaireSelectedInspectionId;
            RaisePropertyChanged("CurrentInspectionId");

            Records = new List<QuestionnaireRecord>();

			_questionnaireModules = new Dictionary<int, QuestionnaireControlData>();
			_questionnaireModules.Add(1,
				new QuestionnaireControlData(1, "Aantal voertuigen", new VehicleCountControl(1, this)));
			_questionnaireModules.Add(2,
				new QuestionnaireControlData(2, "Vragenlijst notities", new QuestionnaireCommentControl(2, this)));

			AddModuleToQuestionnaire = new RelayCommand<int>(AddListElement);
			ListElements = new ObservableCollection<UIElement>();

			SaveInspectionRelay = new RelayCommand(SaveInspection);
		}

		// Retrieve modules and questionsanswers of the inspection (if any)
		public void LoadInspection(int InspectionID)
		{

			

			//using (var context = new LocalParkInspectEntities())
			//{

			//	var Inspection = context.Inspection.SingleOrDefault(i => i.Id == CurrentInspectionId);

			//	questions = context.Questionnaire.SingleOrDefault(q => q.InspectionId == CurrentInspectionId);

			//}

		}

		// Variables used for data handling and database writes

		#region Variables for database


		private readonly List<QuestionnaireRecord> Records;

		#endregion

		// Variables used for handling the view and QuestionnaireModules


		#region Variables for view

		private bool _questionnaireEditingMode = true;

		public bool QuestionnaireEditingMode
		{
			get { return _questionnaireEditingMode; }
			set
			{
				_questionnaireEditingMode = value;
				ShowEditingTools(value);
			}
		}

        public long CurrentInspectionId
        {
            get { return _currentInspectionId;  }
        }

		public Dictionary<int, QuestionnaireControlData> _questionnaireModules;

		public Dictionary<int, QuestionnaireControlData> QuestionnaireModules
		{
			get { return _questionnaireModules; }
		}

		public ObservableCollection<UIElement> ListElements { get; set; }

		public RelayCommand SaveInspectionRelay { get; set; }

		#endregion

		#region Functions for database

		public int GetRecordValue(int moduleId, string[] keywords)
		{
			foreach (var record in Records)
				if (record.ModuleId == moduleId)
					if (CheckEqualKeywordSet(record.Keywords, keywords))
						return record.value;

			return 0;
		}

		public void AddOrUpdateRecord(int moduleId, string[] keywords, int value)
		{
			// Check whether record already exists and edit it's value
			foreach (var record in Records)
				if (record.ModuleId == moduleId)
					if (CheckEqualKeywordSet(record.Keywords, keywords))
					{
						record.value = value;
						return;
					}

			// Else create new record and edit it's value
			Records.Add(new QuestionnaireRecord(moduleId, keywords, value));
		}

		private bool CheckEqualKeywordSet(string[] firstSet, string[] secondSet)
		{
			if (firstSet.Length != secondSet.Length)
				return false;

			var secondSetAsList = new List<string>(secondSet);

			foreach (var first in firstSet)
				if (!secondSetAsList.Contains(first))
					return false;

			return true;
		}

		private void SaveInspection()
		{
			using (var context = new LocalParkInspectEntities())
			{
                var questionnaire = new Questionaire // "Questionaire" spelled wrong due to incorrect spelling in localdatabase
                {
                    Id = (from q in context.Questionaire select q.Id).Max()+1,
                    InspectionId = (int)CurrentInspectionId
                };

				var questionnaireId = context.Questionaire.Add(questionnaire).Id; // "Questionaire" spelled wrong due to incorrect spelling in localdatabase


				// For each QuestionnaireRecord
				foreach (var QR in Records)
				{
                    // Create new Question
                    var question = new Question
                    {
                        Id = (from q in context.Question select q.Id).Max() + 1,
                        Description = "There's nothing here yet!",
                        SortId = 1 // needs to be specified for proper use
                    };

					context.Question.Add(question);

					// Create QuestionKeywords for each keyword in record (QuestionID, KeywordID)
					foreach (var keyword in QR.Keywords)
					{
                        var questionkeyword = new QuestionKeyword
                        {
                            KeywordId = (from k in context.Keyword where k.Description == keyword select k.Id).First(),
                            QuestionId = question.Id
                        };

						context.QuestionKeyword.Add(questionkeyword);
					}

                    // Create QuestionAnswer for the value (QuestionnaireID, QuestionID)
                    var questionanswer = new QuestionAnswer
                    {
                        QuestionnaireId = questionnaireId,
                        QuestionId = question.Id,
                        Result = QR.value.ToString()
                    };

					context.QuestionAnswer.Add(questionanswer);
				}
                try
				{
					context.SaveChanges();
				}
				catch (Exception e)
				{
                    MessageBox.Show(e.Message);
                }
			}

            Navigator.Back();
		}

		#endregion

		#region Functions for view

		public ICommand AddModuleToQuestionnaire { get; set; }

		private void ShowEditingTools(bool b)
		{
			foreach (UserControl control in ListElements)
				(control.DataContext as QuestionnaireModuleViewModelBase).EditingToolsEnabled(b);
		}

		public void BumpElementUp(int moduleId)
		{
			var moduleIndex = ListElements.IndexOf(_questionnaireModules[moduleId].ModuleUserControl);

			if (moduleIndex < 1) return;

			var temp = ListElements[moduleIndex - 1];
			ListElements[moduleIndex - 1] = ListElements[moduleIndex];
			ListElements[moduleIndex] = temp;
			RaisePropertyChanged("ListElements");
		}

		public void BumpElementDown(int moduleId)
		{
			var moduleIndex = ListElements.IndexOf(_questionnaireModules[moduleId].ModuleUserControl);

			if (moduleIndex == ListElements.Count - 1) return;

			var temp = ListElements[moduleIndex + 1];
			ListElements[moduleIndex + 1] = ListElements[moduleIndex];
			ListElements[moduleIndex] = temp;
			RaisePropertyChanged("ListElements");
		}

		public void DeleteListElement(int moduleId)
		{
			ListElements.Remove(_questionnaireModules[moduleId].ModuleUserControl);

			Records.RemoveAll(x => x.ModuleId == moduleId);
		}

		public void AddListElement(int moduleId)
		{
			ListElements.Add(_questionnaireModules[moduleId].ModuleUserControl);
			(_questionnaireModules[moduleId].ModuleUserControl.DataContext as QuestionnaireModuleViewModelBase)
				.EditingToolsEnabled(_questionnaireEditingMode);
			RaisePropertyChanged("ListElements");
			RaisePropertyChanged("OrderedModuleNames");
		}

        // used when a new view is sent to the navigator
        public void RefreshViewModel()
        {
            _currentInspectionId = Settings.Default.QuestionnaireSelectedInspectionId;
            RaisePropertyChanged("CurrentInspectionId");
        }

		#endregion
	}
}