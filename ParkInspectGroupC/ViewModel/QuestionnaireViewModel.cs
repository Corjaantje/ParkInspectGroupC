using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Windows.Controls;
using System.Resources;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View.QuestionnaireModules;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;

namespace ParkInspectGroupC.ViewModel
{
    // krijgt nog een eigen file
    // wil hier eigenlijk geen instantie opslaan; alleen het type om later te instantieren
    public class QuestionnaireModule
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public UserControl ModuleUserControl { get; private set; }

        public QuestionnaireModule(int Id, string Name, UserControl ModuleUserControl)
        {
            this.Id = Id;
            this.Name = Name;
            this.ModuleUserControl = ModuleUserControl;
        }
    }

    public class QuestionnaireRecord
    {
        //public int QuestionId { get; private set; } // unique within inspection
        public int ModuleId { get; private set; } // must be existing module
        public string[] Keywords { get; private set; }
        public int value { get; set; }

        public QuestionnaireRecord(int ModuleId, string[] Keywords, int value)
        {
            this.ModuleId = ModuleId;
            this.Keywords = Keywords;
            this.value = value;
        }
    }

    public class QuestionnaireViewModel : INotifyPropertyChanged
    {
        // Variables used for data handling and database writes
        #region Variables for database

        private int CurrentInspectionId = 100;
        private List<QuestionnaireRecord> Records;

        #endregion

        // Variables used for handling the view and QuestionnaireModules
        #region Variables for view

        private bool _questionnaireEditingMode = true;
        public bool QuestionnaireEditingMode {
            get { return _questionnaireEditingMode; }
            set { _questionnaireEditingMode = value; ShowEditingTools(value); }}

        public Dictionary<int, QuestionnaireModule> _questionnaireModules;
        public Dictionary<int, QuestionnaireModule> QuestionnaireModules
        {
            get { return _questionnaireModules; }
        }

        private ObservableCollection<UIElement> _listElements;
        public ObservableCollection<UIElement> ListElements
        {
            get { return _listElements; }
            set { _listElements = value; }
        }

        public RelayCommand SaveInspectionRelay { get; set; }

        #endregion


        public QuestionnaireViewModel()
        {
            Records = new List<QuestionnaireRecord>();

            _questionnaireModules = new Dictionary<int, QuestionnaireModule>();
            _questionnaireModules.Add(1, new QuestionnaireModule(1, "Aantal voertuigen", new VehicleCountControl(1, this)));
            _questionnaireModules.Add(2, new QuestionnaireModule(2, "Vragenlijst notities", new QuestionnaireCommentControl(2, this)));

            AddModuleToQuestionnaire = new RelayCommand<int>(AddListElement);
            _listElements = new ObservableCollection<UIElement>();

            SaveInspectionRelay = new RelayCommand(SaveInspection);
        }

        #region Functions for database

        public int GetRecordValue(int moduleId, string[] keywords)
        {
            foreach (QuestionnaireRecord record in Records)
            {
                if (record.ModuleId == moduleId)
                {
                    if (CheckEqualKeywordSet(record.Keywords, keywords))
                    {
                        return record.value;
                    }
                }
            }

            return 0;
        }

        public void AddOrUpdateRecord(int moduleId, string[] keywords, int value)
        {
            // Check whether record already exists and edit it's value
            foreach (QuestionnaireRecord record in Records)
            {
                if (record.ModuleId == moduleId)
                {
                    if (CheckEqualKeywordSet(record.Keywords, keywords))
                    {
                        record.value = value;
                        return;
                    }
                }
            }

            // Else create new record and edit it's value
            Records.Add(new QuestionnaireRecord(moduleId, keywords, value));
        }

        private bool CheckEqualKeywordSet(string[] firstSet, string[] secondSet)
        {
            if (firstSet.Length != secondSet.Length)
                return false;

            List<string> secondSetAsList = new List<string>(secondSet);

            foreach (string first in firstSet)
            {
                if (!secondSetAsList.Contains(first))
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveInspection()
        {
            using (var context = new ParkInspectEntities())
            {
                Questionnaire questionnaire = new Questionnaire();
                questionnaire.InspectionId = CurrentInspectionId;
                int questionnaireId = context.Questionnaire.Add(questionnaire).Id;


                // For each QuestionnaireRecord
                foreach (QuestionnaireRecord QR in Records)
                {
                    // Create new Question
                    Question q = context.Question.Add(new Question());

                    // Create QuestionKeywords for each keyword in record (QuestionID, KeywordID)
                    foreach (string keyword in QR.Keywords)
                    {
                        QuestionKeyword kq = new QuestionKeyword();
                        kq.KeywordId = (from k in context.Keyword where k.Description == keyword select k.Id).First();
                        kq.QuestionId = q.Id;
                        context.QuestionKeyword.Add(kq);
                    }

                    // Create QuestionAnswer for the value (QuestionnaireID, QuestionID)
                    QuestionAnswer qa = new QuestionAnswer();
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
            {
                (control.DataContext as QuestionnaireModuleViewModelBase).EditingToolsEnabled(b);
            }
        }

        public void BumpElementUp(int moduleId)
        {
            int moduleIndex = _listElements.IndexOf(_questionnaireModules[moduleId].ModuleUserControl);

            if (moduleIndex < 1) return;

            UIElement temp = _listElements[moduleIndex - 1];
            _listElements[moduleIndex-1]= _listElements[moduleIndex];
            _listElements[moduleIndex] = temp;
            RaisePropertyChanged("ListElements");
        }

        public void BumpElementDown(int moduleId)
        {
            int moduleIndex = _listElements.IndexOf(_questionnaireModules[moduleId].ModuleUserControl);

            if (moduleIndex == _listElements.Count-1) return;

            UIElement temp = _listElements[moduleIndex + 1];
            _listElements[moduleIndex + 1] = _listElements[moduleIndex];
            _listElements[moduleIndex] = temp;
            RaisePropertyChanged("ListElements");
        }

        public void DeleteListElement(int moduleId)
        {
            _listElements.Remove(_questionnaireModules[moduleId].ModuleUserControl);

            Records.RemoveAll(x => x.ModuleId == moduleId);
        }

        public void AddListElement(int moduleId)
        {
            _listElements.Add(_questionnaireModules[moduleId].ModuleUserControl);
            (_questionnaireModules[moduleId].ModuleUserControl.DataContext as QuestionnaireModuleViewModelBase).EditingToolsEnabled(_questionnaireEditingMode);
            RaisePropertyChanged("ListElements");
            RaisePropertyChanged("OrderedModuleNames");
        }

        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
