using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using LocalDatabase.Domain;
using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels
{
    public abstract class QuestionnaireModuleViewModelBase : INotifyPropertyChanged
    {
        private QuestionnaireViewModel qvm;

        private int ModuleId;

        public QuestionnaireModuleViewModelBase()
        { 
            IncrementListPositionRelay = new RelayCommand(IncrementListPosition);
            DecrementListPositionRelay = new RelayCommand(DecrementListPosition);
            DeleteFromQuestionnaireRelay = new RelayCommand(DeleteFromQuestionnaire);
        }

        protected void AddOrUpdateRecord(string[] keywords, int value)
        {
            qvm.AddOrUpdateRecord(ModuleId, keywords, value);
        }

        protected int GetRecordValue(string[] keywords)
        {
            return qvm.GetRecordValue(ModuleId, keywords);
        }

        // should not change once set
        public void SetModuleId(int ModuleId)
        {
            this.ModuleId = ModuleId;
        }

        public void SetQuestionnaireViewModelReference(QuestionnaireViewModel qvm)
        {
            this.qvm = qvm;
        }

        // get a list of all keywords in a KeywordCategory
        protected List<string> GetKeywordList(string category)
        {
            using (var database = new ParkInspectEntities())
            {
                long categoryId = (from cat in database.KeywordCategory where cat.Description == category select cat.Id).SingleOrDefault();

                List<string> keywords = (from kwd in database.Keyword where kwd.CategoryId == categoryId select kwd.Description).ToList();

                return keywords;
            }
        }

        /* Code for common editing tools
         * Only enabled when creating new questionnaire lists
         * 
         * */

        private System.Windows.Visibility _questionnaireEditingToolsVisibility = System.Windows.Visibility.Visible;
        public System.Windows.Visibility QuestionnaireEditingToolsVisibility { get { return _questionnaireEditingToolsVisibility; } }

        public RelayCommand IncrementListPositionRelay { get; set; }
        public RelayCommand DecrementListPositionRelay { get; set; }
        public RelayCommand DeleteFromQuestionnaireRelay { get; set; }

        public void EditingToolsEnabled(bool b)
        {
            if (b)
            {
                _questionnaireEditingToolsVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                _questionnaireEditingToolsVisibility = System.Windows.Visibility.Hidden;
            }

            RaisePropertyChanged("QuestionnaireEditingToolsVisibility");
        }

        protected void IncrementListPosition()
        {
            qvm.BumpElementUp(ModuleId);
        }

        protected void DecrementListPosition()
        {
            qvm.BumpElementDown(ModuleId);
        }

        protected void DeleteFromQuestionnaire()
        {
            CleanupForDeletion();
            qvm.DeleteListElement(ModuleId);
        }

        protected abstract void CleanupForDeletion();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
