using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ParkInspectGroupC.DOMAIN;

namespace ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels
{
    public abstract class QuestionnaireModuleViewModelBase : INotifyPropertyChanged
    {
        /* Code for common editing tools
         * Only enabled when creating new questionnaire lists
         * 
         * */

        private int ModuleId;
        private QuestionnaireViewModel qvm;

        public QuestionnaireModuleViewModelBase()
        {
            IncrementListPositionRelay = new RelayCommand(IncrementListPosition);
            DecrementListPositionRelay = new RelayCommand(DecrementListPosition);
            DeleteFromQuestionnaireRelay = new RelayCommand(DeleteFromQuestionnaire);
        }

        public Visibility QuestionnaireEditingToolsVisibility { get; private set; } = Visibility.Visible;

        public RelayCommand IncrementListPositionRelay { get; set; }
        public RelayCommand DecrementListPositionRelay { get; set; }
        public RelayCommand DeleteFromQuestionnaireRelay { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
                long categoryId =
                    (from cat in database.KeywordCategory where cat.Description == category select cat.Id)
                        .SingleOrDefault();

                var keywords =
                    (from kwd in database.Keyword where kwd.CategoryId == categoryId select kwd.Description).ToList();

                return keywords;
            }
        }

        public void EditingToolsEnabled(bool b)
        {
            if (b)
                QuestionnaireEditingToolsVisibility = Visibility.Visible;
            else
                QuestionnaireEditingToolsVisibility = Visibility.Hidden;

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

        protected void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}