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

    public class QuestionnaireViewModel : INotifyPropertyChanged
    {
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

        public ICommand AddModuleToQuestionnaire { get; set; }


        public QuestionnaireViewModel()
        {
            _questionnaireModules = new Dictionary<int, QuestionnaireModule>();
            _questionnaireModules.Add(1, new QuestionnaireModule(1, "Aantal voertuigen", new VehicleCountControl()));
            _questionnaireModules.Add(2, new QuestionnaireModule(2, "Vragenlijst notities", new QuestionnaireCommentControl()));

            AddModuleToQuestionnaire = new RelayCommand<int>(AddListElement);
            _listElements = new ObservableCollection<UIElement>();
            InitializeVehicleCountControl();

        }

        public void AddListElement(int moduleId)
        {
            _listElements.Add(_questionnaireModules[moduleId].ModuleUserControl);
            RaisePropertyChanged("ListElements");
            RaisePropertyChanged("OrderedModuleNames");
        }

        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;




        /*
         * Dit gaat later verdeeld worden over partials of extra viewmodels
         * 
         * */

        public List<string> VehicleTypes { get; set; }

        public void InitializeVehicleCountControl()
        {
            VehicleTypes = new List<string>();
            VehicleTypes.Add("Personenauto");
            VehicleTypes.Add("Vrachtwagen");

            using (var context = new ParkInspectEntities())
            {
                //var initQuery = (from a in context.Keyword where a.KeywordCategory.Description == "Voertuig" select a.Description);
                //VehicleTypes = initQuery.ToList();
            }

        }
    }
}
