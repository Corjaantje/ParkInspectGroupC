﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.View.QuestionnaireModules;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.ViewModel.Questionnaire;
using GalaSoft.MvvmLight;

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

		// methods and variables for connecting with inspection
		public void setInspection(int InspectionID)
		{

			

			//using (var context = new ParkInspectEntities())
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
			using (var context = new ParkInspectEntities())
			{
				var questionnaire = new DOMAIN.Questionnaire();
				questionnaire.InspectionId = (int)CurrentInspectionId;
				var questionnaireId = context.Questionnaire.Add(questionnaire).Id;


				// For each QuestionnaireRecord
				foreach (var QR in Records)
				{
					// Create new Question
					var q = context.Question.Add(new Question());

					// Create QuestionKeywords for each keyword in record (QuestionID, KeywordID)
					foreach (var keyword in QR.Keywords)
					{
						var kq = new QuestionKeyword();
						kq.KeywordId = (from k in context.Keyword where k.Description == keyword select k.Id).First();
						kq.QuestionId = q.Id;
						context.QuestionKeyword.Add(kq);
					}

					// Create QuestionAnswer for the value (QuestionnaireID, QuestionID)
					var qa = new QuestionAnswer();
					qa.QuestionnaireId = questionnaireId;
					qa.QuestionId = q.Id;
					context.QuestionAnswer.Add(qa);
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